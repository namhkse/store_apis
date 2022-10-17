using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using store_api.Model;

namespace store_api.Services
{
    public class JwtService : IJwtService
    {
        private readonly IAccountService _accountService;

        public JwtService(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public JwtResult GenerateJwt(Account account, string key)
        {
            var jwtDescriptor = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: new Claim[] {
                    new Claim(JwtRegisteredClaimNames.Name, account.UserName),
                    new Claim("role", account.Role.ToString().ToLower())
                },
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)), "HS256")
            );

            var refreshDescriptor = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: new Claim[] {
                new Claim("accountId", account.Id.ToString())
                },
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)), "HS256")
            );

            string accessToken = new JwtSecurityTokenHandler().WriteToken(jwtDescriptor);
            string refreshToken = new JwtSecurityTokenHandler().WriteToken(refreshDescriptor);

            return new JwtResult()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public JwtResult RefreshToken(string refreshToken, string key)
        {
            var validationParams = new TokenValidationParameters()
            {
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)) // The same key as the one that generate the token
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken validatedToken;
            ClaimsPrincipal principal;

            principal = tokenHandler.ValidateToken(refreshToken, validationParams, out validatedToken);

            var claimAccountId = principal.Claims.Where(c => c.Type.Equals("accountId")).FirstOrDefault();
            int accountId;

            if (int.TryParse(claimAccountId.Value, out accountId))
            {
                var account = _accountService.FindAccount(accountId);

                return (account is null) ? null : GenerateJwt(account, key);
            }

            return null;
        }
    }
}
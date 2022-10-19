using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using store_api.Models;

namespace store_api.Services
{
    public class JwtService : IJwtService
    {
        private readonly IAccountService _accountService;
        public const int AccessTokenExpireTimeInMinutes = 1;
        public const int RefreshTokenExpireTimeInMinutes = 5;
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
                    new Claim(JwtClaimTypes.Username, account.Username),
                    new Claim(JwtClaimTypes.UserRole, account.Role.ToString().ToLower())
                },
                expires: DateTime.UtcNow.AddMinutes(AccessTokenExpireTimeInMinutes),
                signingCredentials: new SigningCredentials(GenerateSecurityKey(key), "HS256")
            );

            var refreshDescriptor = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: new Claim[] {
                    new Claim(JwtClaimTypes.AccountId, account.AccountId.ToString())
                },
                expires: DateTime.UtcNow.AddMinutes(RefreshTokenExpireTimeInMinutes),
                signingCredentials: new SigningCredentials(GenerateSecurityKey(key), "HS256")
            );

            string accessToken = new JwtSecurityTokenHandler().WriteToken(jwtDescriptor);
            string refreshToken = new JwtSecurityTokenHandler().WriteToken(refreshDescriptor);

            return new JwtResult()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        /// <summary>
        /// Generate SecurityKey in ASCII Encoding
        /// </summary>
        private SecurityKey GenerateSecurityKey(string rawKey)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(rawKey));
        }

        /// <summary>
        /// Create new JwtResult base on accountId claim in refreshToken.
        /// </summary>
        /// <returns>
        ///  Return new JwtResult or null when fail.
        /// </returns>
        /// <remarks>
        /// Fail: refreshToken is invalid, accountId claim in refreshToken is invalid
        /// </remarks>
        public JwtResult RefreshToken(string refreshToken, string key)
        {
            var validationParams = new TokenValidationParameters()
            {
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = GenerateSecurityKey(key)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken validatedToken;
            ClaimsPrincipal principal;

            principal = tokenHandler.ValidateToken(refreshToken, validationParams, out validatedToken);

            // Get claim accountId in token
            var claimAccountId = principal.Claims.Where(c => c.Type.Equals(JwtClaimTypes.AccountId)).FirstOrDefault();
            int accountId;

            // Is value of claim accountId is valid. In this case it is an integer
            if (int.TryParse(claimAccountId.Value, out accountId))
            {
                var account = _accountService.FindAccount(accountId);

                return (account is null) ? null : GenerateJwt(account, key);
            }

            return null;
        }
    }
}
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

        public string Key
        {
            get => throw new NotImplementedException(); 
            set => throw new NotImplementedException();
        }

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
                    new Claim(JwtClaimTypes.AccountId, account.AccountId.ToString())
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

        public TokenWraper GenerateAccessToken(Account account, string key)
        {
            var expiredTime = DateTime.UtcNow.AddMinutes(AccessTokenExpireTimeInMinutes);

            var jwtDescriptor = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: new Claim[] {
                    new Claim(JwtClaimTypes.AccountId, account.AccountId.ToString()),
                    new Claim(JwtClaimTypes.UserRole, account.Role.RoleName.ToString())
                },
                expires: expiredTime,
                signingCredentials: new SigningCredentials(GenerateSecurityKey(key), "HS256")
            );

            string accessToken = new JwtSecurityTokenHandler().WriteToken(jwtDescriptor);

            return new TokenWraper()
            {
                Token = accessToken,
                ExpiredTime = expiredTime
            };
        }

        public TokenWraper GenerateRefreshToken(Account account, string key)
        {
            var expiredTime = DateTime.UtcNow.AddMinutes(RefreshTokenExpireTimeInMinutes);

            var jwtDescriptor = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: new Claim[] {
                    new Claim(JwtClaimTypes.AccountId, account.AccountId.ToString())
                },
                expires: expiredTime,
                signingCredentials: new SigningCredentials(GenerateSecurityKey(key), "HS256")
            );

            string accessToken = new JwtSecurityTokenHandler().WriteToken(jwtDescriptor);

            return new TokenWraper()
            {
                Token = accessToken,
                ExpiredTime = expiredTime
            };
        }

        public bool CheckValidToken(string token)
        {
            throw new NotImplementedException();
        }

        public bool CheckValidToken(string token, string key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Claim> ExtractClaims(string token, string key)
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

            principal = tokenHandler.ValidateToken(token, validationParams, out validatedToken);

            return principal.Claims;
        }

        public Claim FindClaim(string token, string key, string claimType)
        {
            return ExtractClaims(token, key).FirstOrDefault(c => c.Type.Equals(claimType));
        }
    }
}
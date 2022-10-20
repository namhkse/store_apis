using System.Security.Claims;
using store_api.Models;

namespace store_api.Services
{
    public interface IJwtService
    {
        public TokenWraper GenerateAccessToken(Account account, string key);
        public TokenWraper GenerateRefreshToken(Account account, string key);
        public JwtResult GenerateJwt(Account account, string key);
        public JwtResult RefreshToken(string refreshToken, string key);
        public IEnumerable<Claim> ExtractClaims(string token, string key);
        public Claim FindClaim(string token, string key, string claimType);
        string Key { get; set; }
    }
}
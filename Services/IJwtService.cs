using store_api.Models;

namespace store_api.Services {
    public interface IJwtService
    {
        public JwtResult GenerateJwt(Account account, string key);
        public JwtResult RefreshToken(string refreshToken, string key);
    }
}
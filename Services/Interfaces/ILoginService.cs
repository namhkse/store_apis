using store_api.Models;

namespace store_api.Services
{
    public interface ILoginService
    {
        JwtResult Login(string username, string password);
        JwtResult RefreshLoginWithToken(string refreshToken);
    }
}
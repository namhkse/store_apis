using store_api.Model;

namespace store_api.Services {
    public interface ILoginService {
        JwtResult Login(string userName, string password);
        JwtResult RefreshLoginWithToken(string refreshToken);
    }
}
using store_api.Models;

namespace store_api.Services
{
    public class LoginSerivce : ILoginService
    {
        private readonly IConfiguration _config;
        private readonly IJwtService _jwtService;
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;

        public LoginSerivce(IConfiguration config, IJwtService jwtService, IAccountService accountService)
        {
            _accountService = accountService;
            _config = config;
            _jwtService = jwtService;
        }

        public JwtResult Login(string username, string password)
        {
            Account account = _accountService.FindAccount(username, password);

            if (account is null) return null;

            return _jwtService.GenerateJwt(account, _config["Jwt:Key"]);
        }

        public JwtResult RefreshLoginWithToken(string refreshToken)
        {
            return _jwtService.RefreshToken(refreshToken, _config["Jwt:Key"]);
        }
    }
}
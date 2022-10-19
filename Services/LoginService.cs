using store_api.Models;

namespace store_api.Services
{
    public class LoginSerivce : ILoginService
    {
        private readonly IConfiguration _config;
        private readonly IJwtService _jwtService;
        private readonly IAccountService _accountService;

        public LoginSerivce(IConfiguration config, IJwtService jwtService, IAccountService accountService)
        {
            _accountService = accountService;
            _config = config;
            _jwtService = jwtService;
        }

        public JwtResult Login(string username, string password)
        {
            Account account = _accountService.FindAccount(username, password);

            return (account is null)
                ? null
                : _jwtService.GenerateJwt(account, _config["Jwt:Key"]);
        }

        public JwtResult RefreshLoginWithToken(string refreshToken)
        {
            return _jwtService.RefreshToken(refreshToken, _config["Jwt:Key"]);
        }
    }
}
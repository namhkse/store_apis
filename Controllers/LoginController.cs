using Microsoft.AspNetCore.Mvc;
using store_api.Models;
using store_api.Services;

namespace store_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _config;
        private readonly IAccountService _accountService;
        private readonly IJwtService _jwtService;

        public LoginController(IJwtService jwtService, ITokenService tokenService, IAccountService accountService, IConfiguration config)
        {
            _config = config;
            _accountService = accountService;
            _tokenService = tokenService;
            _jwtService = jwtService;
        }

        [HttpPost]
        public IActionResult Login([FromForm] string username, [FromForm] string password)
        {
            var account = _accountService.FindAccount(username, password);

            if (account is null)
            {
                return Unauthorized("wrong username or passord");
            }

            var accessToken = _jwtService.GenerateAccessToken(account, _config["Jwt:Key"]);
            var refreshToken = _jwtService.GenerateRefreshToken(account, _config["Jwt:Key"]);

            HttpContext.Session.SetString("", "");

            Token t = new Token()
            {
                SessionId = HttpContext.Session.Id,
                AccessToken = accessToken.Token,
                RefreshToken = refreshToken.Token,
                RefreshTokenExpiredTime = refreshToken.ExpiredTime
            };

            _tokenService.Save(t);

            return Ok(t);
        }

        [HttpGet("test")]
        public IActionResult Test() {
            return Ok(new {
                SessionId = HttpContext.Session.Id
            });
        }

        [HttpPost("refresh")]
        public IActionResult RefreshLoginWithToken()
        {
            var token = _tokenService.Find(HttpContext.Session.Id);
            
            if(token is null) return Unauthorized();

            return Ok(null);
        }
    }
}
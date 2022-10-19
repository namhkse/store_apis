using Microsoft.AspNetCore.Mvc;
using store_api.Services;

namespace store_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public IActionResult Login([FromForm] string username, [FromForm] string password)
        {
            var account = _loginService.Login(username, password);

            if (account is null)
            {
                return Unauthorized("wrong username or passord");
            }

            return Ok(account);
        }

        [HttpPost("refresh")]
        public IActionResult RefreshLoginWithToken([FromForm] string refreshToken)
        {
            var JwtResult = _loginService.RefreshLoginWithToken(refreshToken);
            return Ok(JwtResult);
        }
    }
}
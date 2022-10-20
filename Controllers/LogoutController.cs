using Microsoft.AspNetCore.Mvc;

namespace store_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogoutController : ControllerBase
    {

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            var options = new CookieOptions();
            options.Expires = null;
            Response.Cookies.Append(".AspNetCore.Session", "", options);
            return Ok("logout");
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using store_api.Services;
using store_api.Models;

namespace store_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult GetAccounts()
        {
            var accounts = _accountService.GetAccounts();
            return Ok(accounts);
        }
    }
}
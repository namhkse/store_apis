using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using store_api.Models;
using store_api.Services;

namespace store_api.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RoleAuthorize : ActionFilterAttribute
    {
        public bool AllowAnonymous { get; set; }
        public string Role { get; set; }
        public RoleAuthorize(string role)
        {
            Role = role;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var tokenBearer = context.HttpContext.Request.Headers.Authorization;

            if (string.IsNullOrEmpty(tokenBearer))
            {
                context.Result = new UnauthorizedResult();
            }
            else
            {
                var token = tokenBearer.First().Replace("Bearer ", "");

                var jwtService = (IJwtService)context.HttpContext.RequestServices.GetService(typeof(IJwtService));
                var config = (IConfiguration)context.HttpContext.RequestServices.GetService(typeof(IConfiguration));
                var accountService = (IAccountService)context.HttpContext.RequestServices.GetService(typeof(IAccountService));

                string accountId = jwtService.FindClaim(token, config["Jwt:Key"], JwtClaimTypes.AccountId).Value;
                var account = accountService.Find(int.Parse(accountId));

                if (!account.Role.RoleName.Equals(Role))
                {
                    context.Result = new ForbidResult();
                }
            }
        }
    }
}
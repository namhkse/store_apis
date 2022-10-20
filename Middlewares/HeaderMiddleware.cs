using store_api.Models;
using store_api.Services;

namespace store_api.Middlewares
{
    public class HeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public HeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ITokenService tokenService)
        {
            Token token = tokenService.Find(context.Session.Id);
            
            if (token is not null)
            {
                context.Request.Headers.Authorization = $"Bearer {token.AccessToken}";
            }

            await _next(context);
        }

    }
}
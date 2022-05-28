using Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using WebUI.Models.User;

namespace WebUI.Middleware
{
    public class UserCookieMiddleware
    {
        private readonly RequestDelegate _next;

        public UserCookieMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context != null)
            {
                var userService = (IUserService)context.RequestServices.GetService(typeof(IUserService));
                var token = context?.Request.Cookies["Token"];
                var user = await userService.GetUserByToken(new TokenUsernameModel() { Token = token });

                if (user != null && !string.IsNullOrEmpty(user.Id))
                {
                    GlobalProperties.User = user;
                }
            }

            await _next(context);
        }
    }
}

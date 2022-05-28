using Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using WebUI.Models.User;

namespace WebUI.Attributes
{
    public class CookiesUserTokenAuthentication : Attribute, IAuthorizationFilter
    {

        private readonly string _roles;

        public CookiesUserTokenAuthentication(string roles)
        {
            _roles = roles;
        }

        public string Roles => _roles;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userService = (IUserService)context.HttpContext.RequestServices.GetService(typeof(IUserService));

            var token = context.HttpContext.Request.Cookies["Token"];
            var res = true;
            if (string.IsNullOrEmpty(token))
            {
                res = false;
            }

            if (res)
            {
                var result = userService.VerifyToken(new TokenUsernameModel() { Token = token });
                result.Wait();
                if (!result.Result)
                {
                    res = false;
                }
                else
                {
                    var roles = userService.GetUserRoles(new TokenUsernameModel() { Token = token });
                    roles.Wait();

                    var expectedRoles = roles.Result.ToLower().Replace(" ", "").Split(',');
                    var actualRoles = Roles.ToLower().Replace(" ", "").Split(',');

                    if (!expectedRoles.Any(x => actualRoles.FirstOrDefault(y => y.Equals(x)) != null))
                    {
                        res = false;
                    }
                }
            }

            if (res && GlobalProperties.User == null)
            {
                var user = userService.GetUserByToken(new TokenUsernameModel() { Token = token });
                user.Wait();

                GlobalProperties.User = user.Result;

            }

            if (!res)
            {
                context.ModelState.AddModelError("Unauthorized", "You are not authorized");
                context.Result = new UnauthorizedObjectResult(context.ModelState);
            }
        }
    }
}

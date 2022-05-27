using IDS.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace IDS.Attributes
{
    public class CustomTokenAuthentication : Attribute, IAuthorizationFilter
    {
        public string Roles { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var tokenManager = (ITokenManager)context.HttpContext.RequestServices.GetService(typeof(ITokenManager));

            var res = true;
            if (!context.HttpContext.Request.Headers.ContainsKey("Authorization") &&
                !context.HttpContext.Request.Query.ContainsKey("token"))
            {
                res = false;
            }

            if (res)
            {
                var token = context.HttpContext.Request.Headers["Authorization"];
                    //?? .FirstOrDefault(x => x.Key == "token").Value;
                if (string.IsNullOrEmpty(token))
                {
                    token = context.HttpContext.Request.Query["token"];
                }

                if(token.ToString().Contains("Bearer "))
                {
                    token = token.ToString().Replace("Bearer ", "");
                }
                var result = tokenManager.VerifyToken(token);
                result.Wait();
                if (!result.Result)
                {
                    res = false;
                }
                else
                {
                    if (!string.IsNullOrEmpty(Roles))
                    {
                        Roles = "Owner, Admin";
                    }

                    var roles = tokenManager.GetUserRoles(token);
                    roles.Wait();

                    var expectedRoles = roles.Result.ToLower().Trim().Split(',');
                    var actualRoles = Roles.ToLower().Trim().Split(',');

                    if (!expectedRoles.Any(x => actualRoles.FirstOrDefault(y => y.Equals(x)) != null))
                    {
                        res = false;
                    }
                }
            }

            if (!res)
            {
                context.ModelState.AddModelError("Unauthorized", "You are not authorized");
                context.Result = new UnauthorizedObjectResult(context.ModelState);
            }
        }
    }
}

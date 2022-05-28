using Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Attributes;
using WebUI.Models.User;

namespace WebUI.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {

        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        #region Login

        [HttpGet("Login")]
        public async Task<IActionResult> LoginView()
        {
            if (GlobalProperties.User != null)
            {
                return View("../_Error", "You must logout before");
            }

            return View("_LoginView");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginPostAction(LoginUserModel loginUser)
        {
            if (GlobalProperties.User != null)
            {
                return View("../_Error", "You must logout before");
            }

            var token = await _userService.Login(loginUser);

            if (string.IsNullOrEmpty(token))
            {
                return View("../_Error", "Unsuccessfull auth");
            }

            var user = await _userService.GetUserByToken(new TokenUsernameModel() { Token = token });
            if (user == null)
            {
                return View("../_Error", "Unsuccessfull auth");
            }

            Response.Cookies.Append("Token", token);

            return Redirect("../User/Index");
        }

        #endregion

        #region Register

        [HttpGet("Register")]
        public async Task<IActionResult> RegisterView()
        {
            if (GlobalProperties.User != null)
            {
                return View("../_Error", "You must logout before");
            }

            return View("_RegisterView");
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterPostAction(RegisterUserModel regUser)
        {
            if (GlobalProperties.User != null)
            {
                return View("../_Error", "You must logout before");
            }

            var token = await _userService.Register(regUser);

            if (string.IsNullOrEmpty(token))
            {
                return View("../_Error", "Unsuccessfull auth");
            }

            var user = await _userService.GetUserByToken(new TokenUsernameModel() { Token = token });
            if (user == null)
            {
                return View("../_Error", "Unsuccessfull auth");
            }

            Response.Cookies.Append("Token", token);

            return Redirect("../User/Index");
        }

        #endregion

        [CookiesUserTokenAuthentication("Owner, Admin, User")]
        [HttpGet("Logout")]
        public async Task<IActionResult> LogoutAction()
        {
            if (GlobalProperties.User != null)
            {
                var res = await _userService.Logout(new TokenUsernameModel()
                {
                    Username = GlobalProperties.User.UserName,
                    Token = Request.Cookies["Token"]
                });
                if (res)
                {
                    HttpContext.Response.Cookies.Delete("Token");
                    Request.Cookies.Append(new KeyValuePair<string, string>("Token", "Empty"));
                }
            }

            return Redirect("../Guest/Index");
        }
    }
}

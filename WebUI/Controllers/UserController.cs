using Microsoft.AspNetCore.Mvc;
using WebUI.Attributes;

namespace WebUI.Controllers
{
    [CookiesUserTokenAuthentication("Owner, Admin, User")]
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

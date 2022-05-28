using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class GuestController : Controller
    {
        public IActionResult Index()
        {
            if (GlobalProperties.User != null)
            {
                return Redirect("User/Index");
            }

            return View();
        }
    }
}

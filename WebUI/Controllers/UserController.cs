using Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebUI.Attributes;
using WebUI.Models.User;

namespace WebUI.Controllers
{
    [CookiesUserTokenAuthentication("Owner, Admin, User")]
    public class UserController : Controller
    {

        private readonly IPetitionService _petitionService;

        public UserController(IPetitionService petitionService)
        {
            _petitionService = petitionService;
        }

        public async Task<IActionResult> Index()
        {
            if (GlobalProperties.User == null)
            {
                return Redirect("Guest/Index");
            }

            var pet = await _petitionService.GetPetitions();
            return View(pet);
        }
    }
}

using Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebUI.Models.User;

namespace WebUI.Controllers
{
    public class GuestController : Controller
    {

        private readonly IPetitionService _petitionService;

        public GuestController(IPetitionService petitionService)
        {
            _petitionService = petitionService;
        }

        public async Task<IActionResult> Index()
        {
            if (GlobalProperties.User != null)
            {
                return Redirect("User/Index");
            }

            var pet = await _petitionService.GetPetitions();
            return View(pet);
        }
    }
}

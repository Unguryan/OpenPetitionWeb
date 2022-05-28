using Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebUI.Attributes;
using WebUI.Models.Petition;
using WebUI.Models.User;

namespace WebUI.Controllers
{
    [CookiesUserTokenAuthentication("Owner, Admin, User")]
    [Route("[controller]")]
    public class PetitionController : Controller
    {

        private readonly IPetitionService _petitionService;

        public PetitionController(IPetitionService petitionService, IUserService userService)
        {
            _petitionService = petitionService;
        }

        [HttpGet("AddPetition")]
        public async Task<IActionResult> AddPetitionView()
        {
            if (GlobalProperties.User == null)
            {
                return View("../_Error", "You must login before");
            }

            return View("_AddPetitionView");
        }

        [HttpPost("AddPetition")]
        public async Task<IActionResult> AddPetitionPostAction(AddPetitionModel petition)
        {
            if (GlobalProperties.User == null)
            {
                return View("../_Error", "You must login before");
            }

            var res = await _petitionService.AddPetition(petition);

            //if (!string.IsNullOrEmpty(res.Id))
            //{
            //    await Response.WriteAsync("<script>alert('sing was successfull')</script>");
            //}

            return Redirect("../User/Index");
        }

        [HttpGet("SignPetition")]
        public async Task<IActionResult> SignPetition(string id)
        {
            if (GlobalProperties.User == null)
            {
                return View("../_Error", "You must login before");
            }

            var res = await _petitionService.AddVoiceToPetition(
                new AddVoiceToPetitionModel() { IdPetition = id, IdUser = GlobalProperties.User.Id });

            //if (!string.IsNullOrEmpty(res.Id))
            //{
            //    await Response.WriteAsync("<script>alert('sing was successfull')</script>");
            //}

            return Redirect("../User/Index");
        }

        [HttpGet("ClosePetition")]
        public async Task<IActionResult> ClosePetition(string id)
        {
            if (GlobalProperties.User == null)
            {
                return View("../_Error", "You must login before");
            }

            var pet = await _petitionService.GetPetition(id);

            if (pet == null || pet.UserId != GlobalProperties.User.Id)
            {
                return View("../_Error", "You cannot close this Petition");
            }

            var res = await _petitionService.ClosePetition(
                new ClosePetitionModel() { IdPetition = id, IdUser = GlobalProperties.User.Id });

            //if (!string.IsNullOrEmpty(res.Id))
            //{
            //    await Response.WriteAsync("<script>alert('sing was successfull')</script>");
            //}

            return Redirect("../User/Index");
        }
    }
}

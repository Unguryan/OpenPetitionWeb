using Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using WebUI.Attributes;
using WebUI.Hubs;
using WebUI.Models.Petition;
using WebUI.Models.User;

namespace WebUI.Controllers
{
    [CookiesUserTokenAuthentication("Owner, Admin, User")]
    [Route("[controller]")]
    public class PetitionController : Controller
    {

        private readonly IPetitionService _petitionService;

        private readonly IHubContext<NotificationHub> _hubContext;

        public PetitionController(IPetitionService petitionService, IHubContext<NotificationHub> hubContext)
        {
            _petitionService = petitionService;
            _hubContext = hubContext;
        }

        [HttpGet("ShowMyPetition")]
        public async Task<IActionResult> ShowMyPetition()
        {
            if (GlobalProperties.User == null)
            {
                //return;
                return View("../_Error", "You must login before");
            }

            var res = await _petitionService.GetUserPetitions(GlobalProperties.User.Id);
            //await TryUpdateModelAsync(res);

            return View("../User/Index", res);
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

            if (!string.IsNullOrEmpty(GlobalProperties.Id))
            {
                await _hubContext.Groups.AddToGroupAsync(GlobalProperties.Id, $"{res.UserId}/{res.Name}/{res.Description}");
            };

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

            await _hubContext.Clients.Groups($"{res.UserId}/{res.Name}/{res.Description}")
                .SendAsync("displayNotification", $"{res.Name} was closed");

            //if (!string.IsNullOrEmpty(res.Id))
            //{
            //    await Response.WriteAsync("<script>alert('sing was successfull')</script>");
            //}

            return Redirect("../User/Index");
        }
    }
}

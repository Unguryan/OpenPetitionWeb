using Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using WebUI.Attributes;
using WebUI.Hubs;
using WebUI.Models.User;

namespace WebUI.Controllers
{
    [CookiesUserTokenAuthentication("Owner, Admin, User")]
    [Route("[controller]")]
    public class UserController : Controller
    {

        private readonly IPetitionService _petitionService;

        private readonly IHubContext<NotificationHub> _hubContext;

        public UserController(IPetitionService petitionService, IHubContext<NotificationHub> hubContext)
        {
            _petitionService = petitionService;
            _hubContext = hubContext;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            if (GlobalProperties.User == null)
            {
                return Redirect("Guest/Index");
            }

            //await _hubContext.Clients.All.SendAsync("displayNotification",
            //    new object[] {$"Test Message from \"{GlobalProperties.User.Name}\""});
            var pet = await _petitionService.GetPetitions();
            return View(pet);
        }

        [HttpPost("SetId")]
        //[AllowAnonymous]
        public async Task SetId([FromBody]string id)
        {
            GlobalProperties.Id = id;
        }

        //[HttpGet("IndexWithModel")]
        //public async Task<IActionResult> IndexWithModel(object model)
        //{
        //    if (GlobalProperties.User == null)
        //    {
        //        return Redirect("Guest/Index");
        //    }

        //    //var pet = await _petitionService.GetPetitions();
        //    return View("Index", model);
        //}
    }
}

using Interfaces.Models.User;

namespace WebUI.Models.User
{
    public class ChangeRolesModel : IChangeRolesModel
    {

        public string UserName { get; set; }

        public string Roles { get; set; }

    }
}

using Interfaces.Models.User;

namespace IDS.Models
{
    public class ChangeRolesModel : IChangeRolesModel
    {

        public string UserName { get; set; }

        public string Roles { get; set; }

    }
}

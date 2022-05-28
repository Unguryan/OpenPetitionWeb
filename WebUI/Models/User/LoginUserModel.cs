using Interfaces.Models.User;
using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.User
{
    public class LoginUserModel : ILoginUserModel
    {

        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

    }
}

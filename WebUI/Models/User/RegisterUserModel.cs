using Interfaces.Models.User;
using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.User
{
    public class RegisterUserModel : IRegisterUserModel
    {

        public string Name { get; set; }

        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}

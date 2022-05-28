using Interfaces.Models.DB;

namespace WebUI.Models.User
{
    public class User : IUser
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public string Roles { get; set; }

    }
}

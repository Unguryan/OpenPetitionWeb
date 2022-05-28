using Interfaces.Models.User;

namespace WebUI.Models.User
{
    public class TokenUsernameModel : ITokenUsernameModel
    {

        public string Username { get; set; }

        public string Token { get; set; }

    }
}

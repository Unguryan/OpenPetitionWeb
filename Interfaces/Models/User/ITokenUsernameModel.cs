namespace Interfaces.Models.User
{
    public interface ITokenUsernameModel
    {

        string Token { get; set; }

        string Username { get; set; }

    }
}
namespace Interfaces.Models.User
{
    public interface IRegisterUserModel
    {

        string Name { get; set; }

        string UserName { get; set; }

        string Password { get; set; }

    }
}
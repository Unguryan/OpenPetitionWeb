using Interfaces.Models.DB;
using Interfaces.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interfaces.Services
{
    public interface IUserService
    {
        Task<string> Register(IRegisterUserModel user);
        
        Task<string> Login(ILoginUserModel user);

        //ByAuth
        Task<bool> Logout(ITokenUsernameModel token);

        Task<bool> VerifyToken(ITokenUsernameModel token);

        #region ByAuth

        Task<IEnumerable<IUser>> GetUsers(ITokenUsernameModel token);

        Task<IUser> GetUser(ITokenUsernameModel token);

        Task<IUser> ChangeRolesUser(IChangeRolesModel changeRolesModel);

        #endregion

        Task<IUser> GetUserByToken(ITokenUsernameModel tokenUsername);

        Task<string> GetUserRoles(ITokenUsernameModel tokenUsername);
    }
}

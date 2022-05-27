using Interfaces.Models.DB;

namespace IDS.Interfaces
{
    public interface IJwtGenerator
    {
        string CreateToken(IUser user, bool rememberMe);
    }
}

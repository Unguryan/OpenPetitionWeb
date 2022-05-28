using Interfaces.Models.DB;
using Interfaces.Models.User;
using Interfaces.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebUI.Models.User;

namespace WebUI.Services
{
    public class UserService : IUserService
    {
        private const string BaseUrl = "http://localhost:5000/User";

        public async Task<string> Register(IRegisterUserModel user)
        {
            HttpClient client = new HttpClient();

            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var res = await client.PostAsync($"{BaseUrl}/Register", data);

            var token = await res.Content.ReadAsStringAsync();

            return token;
        }

        public async Task<string> Login(ILoginUserModel user)
        {
            HttpClient client = new HttpClient();

            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var res = await client.PostAsync($"{BaseUrl}/Login", data);

            var token = await res.Content.ReadAsStringAsync();

            return token;
        }

        //ByAuth
        public async Task<bool> Logout(ITokenUsernameModel token)
        {
            HttpClient client = new HttpClient();

            var json = JsonConvert.SerializeObject(token);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var res = await client.PostAsync($"{BaseUrl}/Logout?token={token.Token}", data);

            var verified = await res.Content.ReadAsStringAsync();

            return bool.Parse(verified);
        }

        public async Task<bool> VerifyToken(ITokenUsernameModel token)
        {
            HttpClient client = new HttpClient();

            var json = JsonConvert.SerializeObject(token);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var res = await client.PostAsync($"{BaseUrl}/VerifyToken", data);

            var verified = await res.Content.ReadAsStringAsync();

            return bool.Parse(verified);
        }

        #region ByAuth

        public async Task<IEnumerable<IUser>> GetUsers(ITokenUsernameModel token)
        {
            return null;
        }

        public async Task<IUser> GetUser(ITokenUsernameModel token)
        {
            return null;
        }

        public async Task<IUser> ChangeRolesUser(IChangeRolesModel changeRolesModel)
        {
            return null;
        }

        #endregion

        public async Task<IUser> GetUserByToken(ITokenUsernameModel tokenUsername)
        {
            HttpClient client = new HttpClient();

            var json = JsonConvert.SerializeObject(tokenUsername);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var res = await client.PostAsync($"{BaseUrl}/GetUserByToken", data);

            var user = await res.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<User>(user);
        }

        public async Task<string> GetUserRoles(ITokenUsernameModel tokenUsername)
        {
            HttpClient client = new HttpClient();

            var json = JsonConvert.SerializeObject(tokenUsername);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var res = await client.PostAsync($"{BaseUrl}/GetUserRoles", data);

            var roles = await res.Content.ReadAsStringAsync();
            return roles;
        }
    }
}

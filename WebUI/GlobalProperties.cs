using Interfaces.Models.DB;
using System.Security.Policy;

namespace WebUI
{
    public static class GlobalProperties
    {

        public static IUser User { get; set; }

        public static string Id { get; set; }

    }
}

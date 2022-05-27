using Interfaces.Models.DB;

namespace IDS.DB.Models
{
    public class Role : IRole
    {
        public string Id { get; set; }

        public string Name { get; set; }

    }
}

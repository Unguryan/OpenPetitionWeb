using IDS.DB;
using IDS.DB.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace IDS.Extensions
{
    public class DBSeed
    {

        private readonly IDS_Context _context;

        private readonly IDataProtectionProvider _provider;

        public DBSeed(IDS_Context context, IDataProtectionProvider provider)
        {
            _context = context;
            _provider = provider;
        }

        public async Task Seed()
        {
            if (!await _context.Roles.AnyAsync())
            {
                var roles = new[]
                {
                    new Role() { Name = "Owner"},
                    new Role() { Name = "Admin"},
                    new Role() { Name = "User"},
                    //new Role() { Name = "Guest"}
                };

                await _context.Roles.AddRangeAsync(roles);
                await _context.SaveChangesAsync();
            }

            if (!await _context.Users.AnyAsync(u => u.UserName == "SuperVisor"))
            {
                var user = new User()
                {
                    UserName = "SuperVisor",
                    Name = "SuperVisor",
                    Roles = "Owner",
                    PasswordHash = "SuperVisor".EncryptePassword(_provider),
                };

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}

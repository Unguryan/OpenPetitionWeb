using IDS.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace IDS.DB
{
    public class IDS_Context : DbContext
    {
        public IDS_Context(DbContextOptions<IDS_Context> options) : base(options)
        {

        }

        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserToken> Tokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<UserToken>().ToTable("Tokens");

            modelBuilder.Entity<Role>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<User>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<UserToken>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
        }
    }
}

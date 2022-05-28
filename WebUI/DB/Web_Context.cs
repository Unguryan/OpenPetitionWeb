using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using WebUI.Models.Petition;

namespace WebUI.DB
{
    public class Web_Context : DbContext
    {
        public Web_Context(DbContextOptions<Web_Context> options) : base(options)
        {

        }

        public DbSet<Petition> Petitions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Petition>().ToTable("Petitions");

            modelBuilder.Entity<Petition>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Petition>()
            .Property(e => e.CurrentVoices)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());
        }
    }
}

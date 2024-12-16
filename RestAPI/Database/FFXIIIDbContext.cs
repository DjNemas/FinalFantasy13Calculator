using Microsoft.EntityFrameworkCore;
using RestAPI.Database.Models;

namespace RestAPI.Database
{
    public class FFXIIIDbContext : DbContext
    {
        public FFXIIIDbContext(DbContextOptions<FFXIIIDbContext> options) : base(options) {}

        public DbSet<Accessoire> Accessoires { get; set; }
        public DbSet<Weapon> Weapons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Item>().HasIndex(i => i.Name).IsUnique();
        }
    }
}

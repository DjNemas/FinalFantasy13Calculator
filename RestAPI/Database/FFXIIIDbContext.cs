namespace Shared.Database
{
    public class FFXIIIDbContext : DbContext
    {
        public FFXIIIDbContext(DbContextOptions<FFXIIIDbContext> options) : base(options) { }

        public DbSet<Accessoire> Accessoires { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Item>().HasIndex(i => i.Name).IsUnique();

            modelBuilder.Entity<UserRole>().Property(x => x.Role).HasConversion<string>();

            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { Id = 1, Role = Roles.Administrator },
                new UserRole { Id = 2, Role = Roles.User }
            );



        }
    }
}

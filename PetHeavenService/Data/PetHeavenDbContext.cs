using Microsoft.EntityFrameworkCore;
using PetHeavenService.Objects;

namespace PetHeavenService.Data
{
    public class PetHeavenDbContext : DbContext
    {
        public DbSet<Pet> Pets { get; set; } = null;
        public DbSet<User> Users { get; set; } = null;
        public DbSet<PetImage> PetImages { get; set; } = null;
        public DbSet<PetType> PetTypes { get; set; } = null;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DevConnection"));
        }
    }
}

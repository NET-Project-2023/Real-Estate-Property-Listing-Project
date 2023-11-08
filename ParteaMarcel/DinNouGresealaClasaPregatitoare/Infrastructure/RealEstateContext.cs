using Microsoft.EntityFrameworkCore;
using Real_estate.Domain.Entities;

namespace Infrastructure
{
    public class RealEstateContext: DbContext
    {
        public DbSet<Property> Properties { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=RealEstate;User Id=postgres;Password=Monster02@;");

        }

    }
}

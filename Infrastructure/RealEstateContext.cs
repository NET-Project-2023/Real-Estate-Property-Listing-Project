using Microsoft.EntityFrameworkCore;
using Real_estate.Domain.Entities;

namespace Infrastructure
{
    public class RealEstateContext: DbContext
    {
        public RealEstateContext(DbContextOptions<RealEstateContext> options) : base(options)
        {

        }       
        public DbSet<Property> Properties { get; set; }
        public DbSet<User> Users { get; set; }



    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PropertyListing.Server.Domain.Entities;

namespace PropertyListing.Server.Infrastructure
{
    public class PropertyListingContext : DbContext
    {
        public PropertyListingContext(DbContextOptions<PropertyListingContext> options) : base(options)
        {
        }
        public DbSet<Property> Properties { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}

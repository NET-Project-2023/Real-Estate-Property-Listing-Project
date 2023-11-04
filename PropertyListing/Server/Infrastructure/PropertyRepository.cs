using Microsoft.EntityFrameworkCore;
using PropertyListing.Server.Domain.Entities;
using PropertyListing.Server.Domain.Interfaces;

namespace PropertyListing.Server.Infrastructure
{
    public class PropertyRepository : GenericRepository<Property>, IPropertyRepository
    {
        public PropertyRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}

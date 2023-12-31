using Microsoft.EntityFrameworkCore;
using Real_estate.Application.Features.Properties.Queries;
using Real_estate.Application.Persistence;
using Real_estate.Domain.Common;
using Real_estate.Domain.Entities;

namespace Infrastructure.Repositories
{
    public class PropertyRepository : BaseRepository<Property>, IPropertyRepository
    {
        public PropertyRepository(RealEstateContext context) : base(context)
        {
        }

        public async Task<Result<List<Property>>> FindByOwnerUsernameAsync(string ownerUsername)
        {
            try
            {
                // p.UserId is actually ownerUsername
                var properties = await context.Properties.Where(p => p.UserId == ownerUsername).ToListAsync();
                return Result<List<Property>>.Success(properties);
            }  
            catch (Exception ex)
            {
                return Result<List<Property>>.Failure($"Error finding properties by ownerUsername: {ex.Message}");
            }
        }
        
    }
}

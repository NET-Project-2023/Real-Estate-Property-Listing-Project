using Real_estate.Application.Persistence;
using Real_estate.Domain.Entities;

namespace Infrastructure.Repositories
{
    public class PropertyRepository : BaseRepository<Property>, IPropertyRepository
    {
        public PropertyRepository(RealEstateContext context) : base(context)
        {

        }

    }
}

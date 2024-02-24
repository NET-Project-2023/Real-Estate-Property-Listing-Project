using Real_estate.Application.Persistence;
using Real_estate.Domain.Entities;

namespace Infrastructure.Repositories
{
    public class ListingRepository : BaseRepository<Listing>, IListingRepository
    {
        public ListingRepository(RealEstateContext context) : base(context)
        {
        }

    }
}

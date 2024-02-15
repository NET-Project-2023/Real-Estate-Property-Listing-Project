using MediatR;
using Real_estate.Application.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Real_estate.Application.Features.Listings.Queries.GetById
{
    public class GetByIdListingHandler : IRequestHandler<GetByIdListingQuery, ListingDto>
    {
        private readonly IListingRepository repository;

        public GetByIdListingHandler(IListingRepository repository)
        {
            this.repository = repository;
        }

        public async Task<ListingDto> Handle(GetByIdListingQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.FindByNameAsync(request.title);
            if (result.IsSuccess)
            {
                return new ListingDto
                {
                    ListingId = result.Value.ListingId,
                    Title = result.Value.Title,
                    Username = result.Value.Username,
                    PropertyId = result.Value.PropertyId,
                    Price = result.Value.Price,
                    PropertyStatus = result.Value.PropertyStatus
                };
            }
            return null; 
        }
    }
}
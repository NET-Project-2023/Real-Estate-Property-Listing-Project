using MediatR;
using Real_estate.Application.Persistence;

namespace Real_estate.Application.Features.Listings.Queries.GetAll
{
    public class GetAllListingQueryHandler : IRequestHandler<GetAllListingQuery, GetAllListingResponse>
    {
        private readonly IListingRepository repository;

        public GetAllListingQueryHandler(IListingRepository repository)
        {
            this.repository = repository;
        }

        public async Task<GetAllListingResponse> Handle(GetAllListingQuery request, CancellationToken cancellationToken)
        {
            GetAllListingResponse response = new();
            var result = await repository.GetAllAsync();
            if (result.IsSuccess)
            {
                response.Listings = result.Value.Select(c => new ListingDto
                {
                    ListingId = c.ListingId,
                    Title = c.Title,
                    UserId = c.User.UserId,
                    PropertyId = c.Property.PropertyId,
                    Description = c.Description
                }).ToList();
            }
            return response;
        }
    }
}
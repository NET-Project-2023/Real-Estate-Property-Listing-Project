using MediatR;
using Real_estate.Application.Features.Properties.Queries.GetById;
using Real_estate.Application.Features.Properties.Queries;
using Real_estate.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var result = await repository.FindByIdAsync(request.Id);
            if (result.IsSuccess)
            {
                return new ListingDto
                {
                    ListingId = result.Value.ListingId,
                    Title = result.Value.Title,
                    UserId = result.Value.UserId,
                    PropertyId = result.Value.PropertyId,
                    Description = result.Value.Description,
                };
            }
            return new ListingDto();
        }
    }
}

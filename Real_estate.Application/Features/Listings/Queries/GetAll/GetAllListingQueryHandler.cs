using MediatR;
using Real_estate.Application.Features.Properties.Queries.GetAll;
using Real_estate.Application.Features.Properties.Queries;
using Real_estate.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    ListingId= c.ListingId,
                    Title= c.Title,
                    UserId=c.UserId,
                    PropertyId = c.PropertyId,
                   Description= c.Description
                }).ToList();
            }
            return response;
        }
    }
}

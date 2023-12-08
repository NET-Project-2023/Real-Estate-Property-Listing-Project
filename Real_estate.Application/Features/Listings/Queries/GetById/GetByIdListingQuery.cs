using MediatR;
using Real_estate.Application.Features.Listings.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_estate.Application.Features.Listings.Queries.GetById
{
    public record GetByIdListingQuery(Guid Id) : IRequest<ListingDto>;
}
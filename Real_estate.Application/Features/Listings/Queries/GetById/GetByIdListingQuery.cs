using MediatR;
using System;

namespace Real_estate.Application.Features.Listings.Queries.GetById
{
    public record GetByIdListingQuery(Guid id) : IRequest<ListingDto>;
}
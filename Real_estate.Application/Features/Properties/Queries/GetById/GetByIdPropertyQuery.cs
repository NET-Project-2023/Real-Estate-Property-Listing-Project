using MediatR;

namespace Real_estate.Application.Features.Listings.Queries.GetById
{
    public record GetByIdPropertyQuery(Guid Id) : IRequest<PropertyDto>;
}

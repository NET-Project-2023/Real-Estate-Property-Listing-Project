using MediatR;

namespace Real_estate.Application.Features.Properties.Queries.GetById
{
    public record GetByIdPropertyQuery(Guid Id) : IRequest<PropertyDto>;
}

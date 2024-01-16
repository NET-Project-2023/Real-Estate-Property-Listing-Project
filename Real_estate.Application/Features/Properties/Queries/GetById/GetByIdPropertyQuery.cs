using MediatR;
using Real_estate.Application.Features.Properties.Commands;

namespace Real_estate.Application.Features.Properties.Queries.GetById
{
    public record GetByIdPropertyQuery(Guid Id) : IRequest<PropertyDto>;
}

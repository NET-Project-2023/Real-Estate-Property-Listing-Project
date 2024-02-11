using MediatR;
using Real_estate.Application.Features.Properties.Commands;

namespace Real_estate.Application.Features.Properties.Queries.GetByName
{
    public record GetByNamePropertyQuery(string Title) : IRequest<PropertyDto>;
}

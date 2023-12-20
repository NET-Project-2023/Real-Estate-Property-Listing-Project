using MediatR;

namespace Real_estate.Application.Features.Properties.Queries.GetByName
{
    public record GetByNamePropertyQuery(string Title) : IRequest<PropertyDto>;
}

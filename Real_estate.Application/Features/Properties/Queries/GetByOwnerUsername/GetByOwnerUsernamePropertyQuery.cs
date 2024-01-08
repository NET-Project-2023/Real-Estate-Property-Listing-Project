

using MediatR;

namespace Real_estate.Application.Features.Properties.Queries.GetByOwnerUsername
{
    public record GetByOwnerUsernamePropertyQuery(string Username) : IRequest<List<PropertyDto>>;
}

using MediatR;
using Real_estate.Application.Features.Users.Queries.GetById;

namespace Real_estate.Application.Features.Listings.Queries.GetById
{
    public class GetByIdUserQuery : IRequest<GetByIdUserQueryResponse>
    {
        public string Username { get; set; }
    }
}

using Real_estate.Application.Features.Listings;
using Real_estate.Application.Responses;

namespace Real_estate.Application.Features.Users.Queries.GetById
{
    public class GetByIdUserQueryResponse : BaseResponse
    {
        public GetByIdUserQueryResponse() : base()
        {

        }
        public UserDto User { get; set; }
    }
}

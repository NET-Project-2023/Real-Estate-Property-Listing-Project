using Real_estate.Application.Features.Listings;
using Real_estate.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

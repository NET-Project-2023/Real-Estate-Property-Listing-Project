using Real_estate.Application.Responses;

namespace Real_estate.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateListingCommandResponse : BaseResponse
    {
        public UpdateListingCommandResponse() : base()
        {
        }
        public string Message { get; set; }
    }
}

using Real_estate.Application.Responses;

namespace Real_estate.Application.Features.Users.Commands.UpdateUser
{
    public class UpdatePropertyCommandResponse : BaseResponse
    {
        public UpdatePropertyCommandResponse() : base()
        {
        }
        public string Message { get; set; }
    }
}

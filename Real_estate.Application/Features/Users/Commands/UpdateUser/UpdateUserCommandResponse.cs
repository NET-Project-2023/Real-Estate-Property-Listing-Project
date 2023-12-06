using Real_estate.Application.Responses;

namespace Real_estate.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandResponse : BaseResponse
    {
        public UpdateUserCommandResponse() : base()
        {
        }
        public string Message { get; set; }
    }
}

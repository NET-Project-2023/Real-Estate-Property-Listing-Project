using Real_estate.Application.Responses;

namespace Real_estate.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandResponse : BaseResponse
    {
        public CreateUserCommandResponse() : base()
        { 
        }
        public CreateUserDto User { get; set; }
    }
}

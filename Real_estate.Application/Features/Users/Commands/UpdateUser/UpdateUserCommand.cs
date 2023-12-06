using MediatR;
using static Real_estate.Domain.Enums.Enums;

namespace Real_estate.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<UpdateUserCommandResponse>
    {
        public Guid UserId { get; set; }
        public string? Name { get; set; } // Optional update
        public string? Email { get; set; } // Optional update
        public string? Password { get; set; } // Optional update
        public Role? UserRole { get; set; } // Optional update
        public string? PhoneNumber { get; set; } // Optional update
    }
}

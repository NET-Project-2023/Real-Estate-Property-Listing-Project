

using MediatR;
using static Real_estate.Domain.Enums.Enums;

namespace Real_estate.Application.Features.Listings.Commands.CreateUser
{

    public class CreateUserCommand : IRequest<CreateUserCommandResponse>
    {
        public string Name { get; set; } = default!;
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; } = string.Empty;

        public Role UserRole { get; set; }

    }
}

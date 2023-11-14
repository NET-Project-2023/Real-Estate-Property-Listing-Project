using static Real_estate.Domain.Enums.Enums;

namespace Real_estate.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserDto
    {
        public Guid UserId { get; set; }
        public string? Name { get; set; }

        public string? Email { get; set; }   
        public string? Password { get; set; }
        public Role UserRole { get; set; }
        public string? PhoneNumber { get; set; } 


    }

}

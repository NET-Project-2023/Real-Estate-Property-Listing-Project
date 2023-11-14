using Real_estate.Domain.Common;
using static Real_estate.Domain.Enums.Enums;

namespace Real_estate.Domain.Entities
{
    public class User : AuditableEntity
    {
        private User(string name, string email, string password, Role userRole)
        {
            UserId = Guid.NewGuid();
            Name = name;
            Email = email;
            Password = password;
            UserRole = userRole;
        }
        public Guid UserId { get; private set; }
        public string? Name { get; private set; }
        public string? Email { get; private set; }
        public string? Password { get; private set; }
        public Role UserRole { get; private set; }
        public string PhoneNumber { get; private set; } = string.Empty;

        public static Result<User> Create(string name, string email, string password, Role userRole, string? phoneNumber = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Result<User>.Failure("User Name is required.");
            }
            if (string.IsNullOrWhiteSpace(email))
            {
                return Result<User>.Failure("Email is required.");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                return Result<User>.Failure("password is required.");
            }
            if (userRole != Role.Owner && userRole != Role.Customer && userRole != Role.Admin)
            {
                return Result<User>.Failure("Must enter a valid status :  Customer / Owner / Admin ");
            }
            var newUser = new User(name, email, password, userRole);
            if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                newUser.AttachPhoneNumber(phoneNumber);
            }

            return Result<User>.Succes(newUser);
        }


        public void AttachPhoneNumber(string phoneNumber)
        {
            if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                PhoneNumber = phoneNumber;
            }
        }

    }
}

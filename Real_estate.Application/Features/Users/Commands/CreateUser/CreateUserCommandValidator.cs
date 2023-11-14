using FluentValidation;
using System.Text.RegularExpressions;
using static Real_estate.Domain.Enums.Enums;

namespace Real_estate.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            // Validating Name
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{UserName} is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{UserName} must not exceed 100 characters.");

            // Validating Email
            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("{EmailyName} is required.")
                .EmailAddress().WithMessage("Invalid {EmailyName} format.")
                .NotNull();

            // Validating Password
            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("{Password} is required.")
                .NotNull()
                // You might want to include rules for password complexity here
                .MinimumLength(6).WithMessage("{Password} must be at least 6 characters long.");
            
            When(p => !string.IsNullOrWhiteSpace(p.PhoneNumber), () =>
            {
                RuleFor(p => p.PhoneNumber)
                    .Matches(new Regex(@"^\+?[1-9]\d{1,14}$")) // Regex for international phone numbers
                    .WithMessage("{PropertyName} must be in a valid phone number format.");
            });

            // Validating Role - Assuming 'Role' is an enum or similar
            RuleFor(p => p.UserRole)
                .Must(BeAValidRole).WithMessage("{PropertyName} is not a valid role.");

            // Custom method to validate the UserRole enum
            bool BeAValidRole(Role role)
            {
                return Enum.IsDefined(typeof(Role), role);
            }

        }
    }
}

using FluentValidation;
using System.Text.RegularExpressions;
using static Real_estate.Domain.Enums.Enums;

namespace Real_estate.Application.Features.Listings.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            // Validating Name
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage(ValidationMessages.RequiredMessage)
                .NotNull()
                .MaximumLength(100).WithMessage(ValidationMessages.MaxLengthMessage);

            RuleFor(p => p.UserName)
                .NotEmpty().WithMessage(ValidationMessages.RequiredMessage)
                .NotNull()
                .MaximumLength(100).WithMessage(ValidationMessages.MaxLengthMessage);

            // Validating Email
            RuleFor(p => p.Email)
                .NotEmpty().WithMessage(ValidationMessages.RequiredMessage)
                .EmailAddress().WithMessage(ValidationMessages.EmailFormatMessage)
                .NotNull();

            // Validating Password
            RuleFor(p => p.Password)
                .NotEmpty().WithMessage(ValidationMessages.RequiredMessage)
                .NotNull()
                .MinimumLength(6).WithMessage(ValidationMessages.MinLengthMessage);

            When(p => !string.IsNullOrWhiteSpace(p.PhoneNumber), () =>
            {
                RuleFor(p => p.PhoneNumber)
                    .Matches(new Regex(@"^\+?[1-9]\d{1,14}$")) // Regex for international phone numbers
                    .WithMessage(ValidationMessages.PhoneNumberFormatMessage);
            });

            // Validating Role - Assuming 'Role' is an enum or similar
            RuleFor(p => p.UserRole)
                .Must(BeAValidRole).WithMessage(ValidationMessages.NotValidRoleMessage);

            // Custom method to validate the UserRole enum
            bool BeAValidRole(Role role)
            {
                return Enum.IsDefined(typeof(Role), role);
            }

        }
    }
}

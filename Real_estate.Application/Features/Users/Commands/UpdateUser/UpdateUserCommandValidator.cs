using FluentValidation;
using static Real_estate.Domain.Enums.Enums;
using System.Text.RegularExpressions;


namespace Real_estate.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            // Optional validation for Name
            When(p => !string.IsNullOrWhiteSpace(p.Name), () =>
            {
                RuleFor(p => p.Name)
                    .MaximumLength(100).WithMessage(ValidationMessages.MaxLengthMessage);
            });

            // Optional validation for Email
            When(p => !string.IsNullOrWhiteSpace(p.Email), () =>
            {
                RuleFor(p => p.Email)
                    .EmailAddress().WithMessage(ValidationMessages.EmailFormatMessage);
            });

            // Optional validation for Password - assuming you allow password updates
            When(p => !string.IsNullOrWhiteSpace(p.Password), () =>
            {
                RuleFor(p => p.Password)
                    .MinimumLength(6).WithMessage(ValidationMessages.MinLengthMessage);
            });

            // Conditional validation for Phone Number
            When(p => !string.IsNullOrWhiteSpace(p.PhoneNumber), () =>
            {
                RuleFor(p => p.PhoneNumber)
                    .Matches(new Regex(@"^\+?[1-9]\d{1,14}$")) // Regex for international phone numbers
                    .WithMessage(ValidationMessages.PhoneNumberFormatMessage);
            });

            // Conditional validation for UserRole
            When(p => p.UserRole.HasValue, () =>
            {
                RuleFor(p => p.UserRole.Value)
                    .Must(BeAValidRole).WithMessage(ValidationMessages.NotValidRoleMessage);
            });
        }

        private bool BeAValidRole(Role role)
        {
            return Enum.IsDefined(typeof(Role), role);
        }
    }
}

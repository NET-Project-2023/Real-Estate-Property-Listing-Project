using FluentValidation;

namespace Real_estate.Application.Features.Listings.Commands.CreateListing
{
    public class CreateListingCommandValidator : AbstractValidator<CreateListingCommand>
    {
        public CreateListingCommandValidator()
        {
            // Validating Title
            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(200).WithMessage("{PropertyName} must not exceed 200 characters.");

            // Validating UserId
            RuleFor(p => p.UserId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
                // check if UserId exists

            // Validating PropertyId
            RuleFor(p => p.PropertyId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .Must(BeAValidGuid).WithMessage("{PropertyName} must be a valid GUID.");

            // Validating Description
            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            // Validating PropertyStatus
            

            bool BeAValidGuid(Guid guid)
            {
                return guid != Guid.Empty;
            }
        }
    }
}

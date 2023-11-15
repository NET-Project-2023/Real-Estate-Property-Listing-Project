using FluentValidation;


namespace Real_estate.Application.Features.Properties.Commands.CreateProperty
{
    public class CreatePropertyCommandValidator : AbstractValidator<CreatePropertyCommand>
    {
        public CreatePropertyCommandValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("{PropertyTitle} is required")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyTitle} must not exceed 100 characters.");

            RuleFor(p => p.Address)
                .NotEmpty().WithMessage("{Address} is required")
                .NotNull()
                .MaximumLength(150).WithMessage("{PropertyAddress} must not exceed 150 characters.");

            RuleFor(p => p.Size)
           .GreaterThan(0).WithMessage("{Size} must be greater than 0");

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("{Price} must be greater than 0");

            RuleFor(p => p.NumberOfBedrooms)
                .GreaterThan(0).WithMessage("{NumberOfBedrooms} must be greater than 0");

            RuleFor(p => p.NumberOfBathrooms)
                .GreaterThan(0).WithMessage("{NumberOfBathrooms} must be greater than 0");

            RuleFor(p => p.ImagesUrls)
                .Must(urls => urls != null && urls.Any()).When(p => p.ImagesUrls != null)
                .WithMessage("{ImagesUrls} must not be empty");

            RuleFor(p => p.PropertyStatus)
                .IsInEnum().WithMessage("{PropertyStatus} is not a valid status");

            RuleFor(p => p.OwnerId)
                .NotEmpty().WithMessage("{OwnerId} is required")
                .NotEqual(Guid.Empty).WithMessage("{OwnerId} must not be empty");
        }
    }
}
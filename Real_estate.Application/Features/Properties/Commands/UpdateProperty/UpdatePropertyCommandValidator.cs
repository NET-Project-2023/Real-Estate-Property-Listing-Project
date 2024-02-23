using FluentValidation;
using static Real_estate.Domain.Enums.Enums;


namespace Real_estate.Application.Features.Properties.Commands.UpdateProperty
{
    public class UpdatePropertyCommandValidator : AbstractValidator<UpdatePropertyCommand>
    {
        public UpdatePropertyCommandValidator()
        {
            // Optional validation for Title
            When(p => !string.IsNullOrWhiteSpace(p.Title), () =>
            {
                RuleFor(p => p.Title)
                    .MaximumLength(100).WithMessage(ValidationMessages.MaxLengthMessage);
            });

            // Optional validation for Description
            When(p => !string.IsNullOrWhiteSpace(p.Description), () =>
            {
                RuleFor(p => p.Description)
                    .MaximumLength(500).WithMessage(ValidationMessages.MaxLengthMessage);
            });

            When(p => !string.IsNullOrWhiteSpace(p.City), () =>
            {
                RuleFor(p => p.City)
                    .MaximumLength(25).WithMessage(ValidationMessages.MaxLengthMessage);
            });

            When(p => !string.IsNullOrWhiteSpace(p.StreetAddress), () =>
            {
                RuleFor(p => p.StreetAddress)
                    .MaximumLength(100).WithMessage(ValidationMessages.MaxLengthMessage);
            });

            // Optional validation for Size
            When(p => p.Size.HasValue, () =>
            {
                RuleFor(p => p.Size)
                    .GreaterThan(0).WithMessage(ValidationMessages.GreaterThanZeroMessage);
            });


            // Optional validation for NumberOfBedrooms
            When(p => p.NumberOfBedrooms.HasValue, () =>
            {
                RuleFor(p => p.NumberOfBedrooms)
                    .GreaterThan(0).WithMessage(ValidationMessages.GreaterThanZeroMessage);
            });

            // Conditional validation for NumberOfBathrooms
            When(p => p.NumberOfBathrooms.HasValue, () =>
            {
                RuleFor(p => p.NumberOfBathrooms)
                    .GreaterThanOrEqualTo(0).WithMessage(ValidationMessages.GreaterThanZeroMessage);
            });


            RuleFor(p => p.Images)
                 .Must(images => images == null || images.Any())
                    .WithMessage("Images list can be null or must have at least one image.");

        }

        private bool BeAValidStatus(Status status)
        {
            return Enum.IsDefined(typeof(Status), status);
        }
    }
}

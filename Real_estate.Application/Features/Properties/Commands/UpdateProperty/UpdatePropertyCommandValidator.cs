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

            // Optional validation for Address
            When(p => !string.IsNullOrWhiteSpace(p.Address), () =>
            {
                RuleFor(p => p.Address)
                    .MaximumLength(200).WithMessage(ValidationMessages.MaxLengthMessage);
            });

            // Optional validation for Size
            When(p => p.Size.HasValue, () =>
            {
                RuleFor(p => p.Size)
                    .GreaterThan(0).WithMessage(ValidationMessages.GreaterThanZeroMessage);
            });

            // Optional validation for Price
            When(p => p.Price.HasValue, () =>
            {
                RuleFor(p => p.Price)
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

            // Conditional validation for Images

            RuleFor(p => p.Images)
                .Must(images => images != null && images.Any()).When(p => p.Images != null)
                .WithMessage(ValidationMessages.NotEmptyListMessage);


        }

        private bool BeAValidStatus(Status status)
        {
            return Enum.IsDefined(typeof(Status), status);
        }
    }
}

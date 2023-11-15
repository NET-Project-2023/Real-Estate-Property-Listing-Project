using FluentValidation;


namespace Real_estate.Application.Features.Properties.Commands.CreateProperty
{
    public class CreatePropertyCommandValidator : AbstractValidator<CreatePropertyCommand>
    {
        public CreatePropertyCommandValidator()
        {
            RuleFor(p => p.Title)
            .NotEmpty().WithMessage(ValidationMessages.RequiredMessage)
            .NotNull()
            .MaximumLength(100).WithMessage(ValidationMessages.MaxLengthMessage);

            RuleFor(p => p.Address)
                .NotEmpty().WithMessage(ValidationMessages.RequiredMessage)
                .NotNull()
                .MaximumLength(150).WithMessage(ValidationMessages.MaxLengthMessage);

            RuleFor(p => p.Size)
                .GreaterThan(0).WithMessage(ValidationMessages.GreaterThanZeroMessage);

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage(ValidationMessages.GreaterThanZeroMessage);

            RuleFor(p => p.NumberOfBedrooms)
                .GreaterThan(0).WithMessage(ValidationMessages.GreaterThanZeroMessage);

            RuleFor(p => p.NumberOfBathrooms)
                .GreaterThan(0).WithMessage(ValidationMessages.GreaterThanZeroMessage);

            RuleFor(p => p.Images)
                .Must(images => images != null && images.Any()).When(p => p.Images != null)
                .WithMessage(ValidationMessages.NotEmptyListMessage);

            RuleFor(p => p.PropertyStatus)
                .IsInEnum().WithMessage(ValidationMessages.NotValidEnumMessage);

            RuleFor(p => p.OwnerId)
                .NotEmpty().WithMessage(ValidationMessages.RequiredMessage)
                .NotEqual(Guid.Empty).WithMessage(ValidationMessages.NotEmptyGuidMessage);
        }
    }
    
}
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
            .MaximumLength(150).WithMessage(ValidationMessages.MaxLengthMessage);

            RuleFor(p => p.City)
                .NotEmpty().WithMessage(ValidationMessages.RequiredMessage)
                .NotNull()
                .MaximumLength(50).WithMessage(ValidationMessages.MaxLengthMessage);

            RuleFor(p => p.StreetAddress)
                .NotEmpty().WithMessage(ValidationMessages.RequiredMessage)
                .NotNull()
                .MaximumLength(50).WithMessage(ValidationMessages.MaxLengthMessage);


            RuleFor(p => p.Size)
                .GreaterThan(0).WithMessage(ValidationMessages.GreaterThanZeroMessage);


            RuleFor(p => p.NumberOfBedrooms)
                .GreaterThan(0).WithMessage(ValidationMessages.GreaterThanZeroMessage);

            RuleFor(p => p.NumberOfBathrooms)
                .GreaterThan(0).WithMessage(ValidationMessages.GreaterThanZeroMessage);

            //RuleFor(p => p.Images)
            //    .Must(images => images != null && images.Any()).When(p => p.Images != null)
            //    .WithMessage(ValidationMessages.NotEmptyListMessage);


            RuleFor(p => p.UserId)
                .NotEmpty().WithMessage(ValidationMessages.RequiredMessage)
                .NotEqual(string.Empty).WithMessage(ValidationMessages.NotEmptyGuidMessage);
        }
    }
    
}
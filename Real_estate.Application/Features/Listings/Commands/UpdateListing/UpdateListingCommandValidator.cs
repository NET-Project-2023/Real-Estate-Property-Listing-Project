using FluentValidation;


namespace Real_estate.Application.Features.Listings.Commands.UpdateListing
{
    public class UpdateListingCommandValidator : AbstractValidator<UpdateListingCommand>
    {
        public UpdateListingCommandValidator()
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

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero");
        }
    }
}

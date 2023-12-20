using MediatR;
using Real_estate.Application.Persistence;
using Real_estate.Domain.Entities;

namespace Real_estate.Application.Features.Properties.Commands.UpdateProperty
{
    public class UpdatePropertyCommandHandler : IRequestHandler<UpdatePropertyCommand, UpdatePropertyCommandResponse>
    {
        private readonly IAsyncRepository<Property> propertyRepository;

        public UpdatePropertyCommandHandler(IAsyncRepository<Property> propertyRepository)
        {
            this.propertyRepository = propertyRepository;
        }

        public async Task<UpdatePropertyCommandResponse> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdatePropertyCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new UpdatePropertyCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var propertyResult = await propertyRepository.FindByNameAsync(request.Title);
            if (!propertyResult.IsSuccess)
            {
                return new UpdatePropertyCommandResponse
                {
                    Success = false,
                    Message = "Property not found."
                };
            }

            var property = propertyResult.Value;
            if (request.Title != null)
            {
                property.UpdateTitle(request.Title);
            }
            if (request.Description != null)
            {
                property.AttachDescription(request.Description);
            }
            if (request.Address != null)
            {
                property.UpdateAddress(request.Address);
            }
            if (request.Size.HasValue)
            {
                property.UpdateSize(request.Size.Value);
            }
            if (!string.IsNullOrEmpty(request.Description))
            {
                property.UpdateDescription(request.Description);
            }
            if (request.Price.HasValue)
            {
                property.UpdatePrice(request.Price.Value);
            }
            if (request.NumberOfBedrooms.HasValue)
            {
                property.UpdateNumberOfBedrooms(request.NumberOfBedrooms.Value);
            }
            if (request.NumberOfBathrooms.HasValue)
            {
                property.AttachNumberOfBathrooms(request.NumberOfBathrooms.Value);
            }
            if (request.Images != null && request.Images.Any())
            {
                property.AttachImageUrls(request.Images);
            }

            var updateResult = await propertyRepository.UpdateAsync(property);

            if (!updateResult.IsSuccess)
            {
                return new UpdatePropertyCommandResponse
                {
                    Success = false,
                    Message = "Failed to update property."
                };
            }

            return new UpdatePropertyCommandResponse
            {
                Success = true,
                Message = "Property updated successfully."
            };
        }
    }
}

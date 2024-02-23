using MediatR;
using Microsoft.Extensions.Logging;
using Real_estate.Application.Persistence;
using Real_estate.Domain.Entities;

namespace Real_estate.Application.Features.Properties.Commands.CreateProperty
{
    public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, CreatePropertyCommandResponse>
    {
        private readonly IPropertyRepository propertyRepository;
        private readonly IUserManager userRepository;
        private readonly ILogger<CreatePropertyCommandHandler> _logger;


        public CreatePropertyCommandHandler(IPropertyRepository propertyRepository, IUserManager userRepository, ILogger<CreatePropertyCommandHandler> logger)
        {
            this.propertyRepository = propertyRepository;
            this.userRepository = userRepository;
            _logger = logger;
        }

        public async Task<CreatePropertyCommandResponse> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreatePropertyCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new CreatePropertyCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            // Check if the user exists
            //var userExists = await userRepository.UserExistsAsync(request.UserId);
            var userExists = await userRepository.FindByIdAsync(request.UserId);
            if (userExists == null)
            {
                return new CreatePropertyCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { "User with the specified ID does not exist." }
                };
            }
            var image = request.Images[0];

            var base64Data = Convert.ToBase64String(image);
            //_logger.LogInformation($"Image {0} Base64 data: {base64Data}");

            //_logger.LogInformation($"Received image with byte length: {request.Images}");

            var property = Property.Create(request.Title, request.City, request.StreetAddress, request.Size, request.UserId, request.NumberOfBedrooms, request.Images);
            if (!property.IsSuccess)
            {
                return new CreatePropertyCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { property.Error }
                };
            }

            if (request.Description != null)
            {
                property.Value.AttachDescription(request.Description);
            }

            property.Value.AttachNumberOfBathrooms(request.NumberOfBathrooms);
            property.Value.CreatedBy = request.UserId;
            property.Value.CreatedDate = DateTime.UtcNow;
            property.Value.LastModifiedBy = request.UserId;
            property.Value.LastModifiedDate = DateTime.UtcNow;

            await propertyRepository.AddAsync(property.Value);

            return new CreatePropertyCommandResponse
            {
                Success = true,
                Property = new CreatePropertyDto
                {
                    PropertyId = property.Value.PropertyId,
                    Title = property.Value.Title,
                    Description = property.Value.Description,
                    City = property.Value.City,
                    StreetAddress = property.Value.StreetAddress,
                    Size = property.Value.Size,
                    NumberOfBedrooms = property.Value.NumberOfBedrooms,
                    NumberOfBathrooms = property.Value.NumberOfBathrooms,
                    Images = property.Value.Images,
                    UserId = property.Value.UserId,
                }
            };
        }
    }
}

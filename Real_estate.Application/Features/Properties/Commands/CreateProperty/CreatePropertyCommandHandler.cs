using MediatR;
using Microsoft.Extensions.Logging;
using Real_estate.Application.Persistence;
using Real_estate.Domain.Entities;
using static System.Net.Mime.MediaTypeNames;

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
            _logger.LogInformation($"Image {0} Base64 data: {base64Data}");

            _logger.LogInformation($"Received image with byte length: {request.Images}");

            var property = Property.Create(request.Title, request.Address, request.Size, request.Price, request.UserId, request.NumberOfBedrooms, request.Images);
            if (!property.IsSuccess)
            {
                return new CreatePropertyCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { property.Error }
                };
            }

            await propertyRepository.AddAsync(property.Value);

            return new CreatePropertyCommandResponse
            {
                Success = true,
                Property = new CreatePropertyDto
                {
                    PropertyId = property.Value.PropertyId,
                    Title = property.Value.Title,
                    Address = property.Value.Address,
                    Size = property.Value.Size,
                    Price = property.Value.Price,
                    UserId = property.Value.UserId,
                    NumberOfBedrooms = property.Value.NumberOfBedrooms,
                    Images = property.Value.Images
                }
            };
        }
    }
}

using MediatR;
using Real_estate.Application.Persistence;
using Real_estate.Domain.Entities;
using Real_estate.Application.Features.Properties.Commands;

namespace Real_estate.Application.Features.Properties.Commands.CreateProperty
{
    public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, CreatePropertyCommandResponse>
    {
        private readonly IPropertyRepository propertyRepository;

        public CreatePropertyCommandHandler(IPropertyRepository propertyRepository)
        {
            this.propertyRepository = propertyRepository;
        }
        public async Task<CreatePropertyCommandResponse> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
        {
            //var property = Property.Create(request.PropertyTitle);
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

            var property = Property.Create(request.Title, request.Address, request.Size, request.Price, request.PropertyStatus, request.OwnerId, request.NumberOfBedrooms);
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
                    PropertyStatus = property.Value.PropertyStatus,
                    OwnerId = property.Value.OwnerId,
                    NumberOfBedrooms = property.Value.NumberOfBedrooms

                }
            };
        }
    }
}
using MediatR;
using Real_estate.Application.Persistence;
using Real_estate.Domain.Entities;
using Real_estate.Application.Features.Properties.Commands;
using Real_estate.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Real_estate.Application.Features.Properties.Commands.CreateProperty
{
    public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, CreatePropertyCommandResponse>
    {
        private readonly IPropertyRepository propertyRepository;
        private readonly IUserRepository userRepository; // Add this

        public CreatePropertyCommandHandler(IPropertyRepository propertyRepository, IUserRepository userRepository)
        {
            this.propertyRepository = propertyRepository;
            this.userRepository = userRepository;
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
            var userExists = await userRepository.UserExistsAsync(request.OwnerId);
            if (!userExists)
            {
                return new CreatePropertyCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { "User with the specified ID does not exist." }
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

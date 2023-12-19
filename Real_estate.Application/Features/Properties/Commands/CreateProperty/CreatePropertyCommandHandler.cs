﻿using MediatR;
using Real_estate.Application.Persistence;
using Real_estate.Domain.Entities;

namespace Real_estate.Application.Features.Properties.Commands.CreateProperty
{
    public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, CreatePropertyCommandResponse>
    {
        private readonly IPropertyRepository propertyRepository;
        private readonly IUserManager userRepository;

        public CreatePropertyCommandHandler(IPropertyRepository propertyRepository, IUserManager userRepository)
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

            // TODO: CreatedBy
            var property = Property.Create(request.Title, request.Address, request.Size, request.Price, request.UserId, request.NumberOfBedrooms);
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
                    NumberOfBedrooms = property.Value.NumberOfBedrooms
                }
            };
        }
    }
}

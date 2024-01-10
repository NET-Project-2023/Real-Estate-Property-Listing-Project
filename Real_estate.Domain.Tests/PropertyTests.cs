using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Identity.Models;
using Moq;
using Real_estate.Application.Features.Listings;
using Real_estate.Application.Features.Properties.Commands.CreateProperty;
using Real_estate.Application.Persistence;
using Real_estate.Domain;
using Real_estate.Domain.Common;
using Real_estate.Domain.Entities;
using Xunit;

namespace Real_estate.Domain.Tests
{
    public class PropertyTests
    {
        [Fact]
        public void Create_ValidInput_ShouldCreateProperty()
        {
            // Arrange
            string title = "Sample Property";
            string address = "123 Main St";
            int size = 1000;
            int price = 200000;
            string userId = "user123";
            int numberOfBedrooms = 3;

            // Act
            var result = Property.Create(title, address, size, price, userId, numberOfBedrooms);

            // Assert
            result.IsSuccess.Should().BeTrue();
            var property = result.Value;
            property.Should().NotBeNull();
            property.Title.Should().Be(title);
            property.Address.Should().Be(address);
            property.Size.Should().Be(size);
            property.Price.Should().Be(price);
            property.UserId.Should().Be(userId);
            property.NumberOfBedrooms.Should().Be(numberOfBedrooms);
        }

        [Fact]
        public void Create_InvalidUsername_ShouldReturnFailure()
        {
            // Arrange
            string title = "Sample Property";
            string address = "123 Main St";
            int size = 1000;
            int price = 200000;
            string userId = null;
            int numberOfBedrooms = 3;

            // Act
            var result = Property.Create(title, address, size, price, userId, numberOfBedrooms);

            // Assert
            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public void Create_InvalidAddress_ShouldReturnFailure()
        {
            // Arrange
            string title = "Sample Property";
            string? address = null;
            int size = 1000;
            int price = 200000;
            string userId = "user123";
            int numberOfBedrooms = 3;

            // Act
            var result = Property.Create(title, address=address, size, price, userId, numberOfBedrooms);

            // Assert
            result.IsSuccess.Should().BeFalse();
        }
        [Fact]
        public async Task Handle_ValidCommand_ShouldCreateProperty()
        {
            // Arrange
            var propertyRepositoryMock = new Mock<IPropertyRepository>();
            var userRepositoryMock = new Mock<IUserManager>();

            var handler = new CreatePropertyCommandHandler(propertyRepositoryMock.Object, userRepositoryMock.Object);

            var validCommand = new CreatePropertyCommand
            {
                Title = "Sample Property",
                Address = "123 Main St",
                Size = 1000,
                Price = 200000,
                UserId = "user123",
                NumberOfBedrooms = 3
                // Add other necessary properties
            };

            // Setup userRepositoryMock to return a successful result with an empty UserDto
            userRepositoryMock.Setup(repo => repo.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(Result<UserDto>.Success(new UserDto()));


            // Act
            var result = await handler.Handle(validCommand, CancellationToken.None);

            // Assert
            //result.Success.Should().BeTrue("because the operation should be successful");
            result.Property.Should().NotBeNull("because a property should be created");
            // Add additional assertions for the returned property details

            // Verify that AddAsync was called once on the propertyRepositoryMock
            propertyRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Property>()), Times.Once, "because a property should be added");
        }


        [Fact]
        public async Task Handle_InvalidCommand_ShouldReturnValidationErrors()
        {
            // Arrange
            var propertyRepositoryMock = new Mock<IPropertyRepository>();
            var userRepositoryMock = new Mock<IUserManager>();

            var handler = new CreatePropertyCommandHandler(propertyRepositoryMock.Object, userRepositoryMock.Object);

            var invalidCommand = new CreatePropertyCommand
            {
                // Populate the command with invalid data to trigger validation errors
            };

            // Act
            var result = await handler.Handle(invalidCommand, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.ValidationsErrors.Should().NotBeEmpty();
            // Add additional assertions for the expected validation errors
            propertyRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Property>()), Times.Never);
        }

        [Fact]
        public async Task Handle_UserNotFound_ShouldReturnValidationError()
        {
            // Arrange
            var propertyRepositoryMock = new Mock<IPropertyRepository>();
            var userRepositoryMock = new Mock<IUserManager>();

            var handler = new CreatePropertyCommandHandler(propertyRepositoryMock.Object, userRepositoryMock.Object);

            var commandWithNonExistingUser = new CreatePropertyCommand
            {
                UserId = "nonExistingUserId"
                // Add other necessary properties
            };

            userRepositoryMock.Setup(repo => repo.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(Result<UserDto>.Failure("User with the specified ID does not exist."));

            // Act
            var result = await handler.Handle(commandWithNonExistingUser, CancellationToken.None);

            // Assert
            result.Should().BeOfType<CreatePropertyCommandResponse>();
            result.Success.Should().BeFalse();
            result.ValidationsErrors.Should().Contain("'Address' must not be empty.");

            propertyRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Property>()), Times.Never);
        }
        [Fact]
        public void Create_InvalidSize_ShouldReturnFailure()
        {
            // Arrange
            string title = "Sample Property";
            string address = "123 Main St";
            int size = 0; // Invalid size
            int price = 200000;
            string userId = "user123";
            int numberOfBedrooms = 3;

            // Act
            var result = Property.Create(title, address, size, price, userId, numberOfBedrooms);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Contain("Size must be greater than 0.");
        }

        [Fact]
        public void Create_InvalidPrice_ShouldReturnFailure()
        {
            // Arrange
            string title = "Sample Property";
            string address = "123 Main St";
            int size = 1000;
            int price = 0; // Invalid price
            string userId = "user123";
            int numberOfBedrooms = 3;

            // Act
            var result = Property.Create(title, address, size, price, userId, numberOfBedrooms);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Contain("Price must be greater than 0.");
        }

        [Fact]
        public void Create_InvalidNumberOfBedrooms_ShouldReturnFailure()
        {
            // Arrange
            string title = "Sample Property";
            string address = "123 Main St";
            int size = 1000;
            int price = 200000;
            string userId = "user123";
            int numberOfBedrooms = 0; // Invalid number of bedrooms

            // Act
            var result = Property.Create(title, address, size, price, userId, numberOfBedrooms);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Contain("Number of bedrooms must be greater than 0.");
        }

        [Fact]
        public void Create_ValidInputWithDescription_ShouldCreatePropertyWithDescription()
        {
            // Arrange
            string title = "Sample Property";
            string address = "123 Main St";
            int size = 1000;
            int price = 200000;
            string userId = "user123";
            int numberOfBedrooms = 3;
            string description = "This is a sample property description.";

            // Act
            var result = Property.CreateWithDescription(title, address, size, price, userId, numberOfBedrooms, description);

            // Assert
            result.IsSuccess.Should().BeTrue();
            var property = result.Value;
            property.Should().NotBeNull();
            property.Description.Should().Be(description);
        }

    }
    // Similar tests for other properties can be added
}


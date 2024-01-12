using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Identity.Models;
using Moq;
using Real_estate.Application.Features.Listings;
using Real_estate.Application.Features.Listings.Commands.CreateListing;
using Real_estate.Application.Features.Properties.Commands.CreateProperty;
using Real_estate.Application.Persistence;
using Real_estate.Domain.Common;
using Real_estate.Domain.Entities;
using Real_estate.Domain.Enums;

using Real_estate.Domain.Entities;
using Xunit;
using static Real_estate.Domain.Enums.Enums;


namespace Real_estate.Domain.Tests
{
    public class ListingTests
    {
        [Fact]
        public void CreateListing_ValidInput_ShouldCreateListing()
        {
            // Arrange
            var listingRepositoryMock = new Mock<IListingRepository>();
            var userRepositoryMock = new Mock<IUserManager>();
            var propertyRepositoryMock = new Mock<IPropertyRepository>();

            var handler = new CreateListingCommandHandler(listingRepositoryMock.Object, userRepositoryMock.Object, propertyRepositoryMock.Object);

            var validCommand = new CreateListingCommand
            {
                Title = "Sample Listing",
                Description = "This is a sample listing.",
                Price = 200000,
                Username = "user123",
                PropertyName = "Sample Property",
                PropertyStatus = Enums.Enums.Status.ForSale

                // Add other necessary properties
            };

            // Setup userRepositoryMock and propertyRepositoryMock to return successful results with empty UserDto and Property respectively
            userRepositoryMock.Setup(repo => repo.FindByUsernameAsync(It.IsAny<string>()))
                .ReturnsAsync(Result<UserDto>.Success(new UserDto()));

            propertyRepositoryMock.Setup(repo => repo.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(Result<Property>.Success(Property.Create("Sample Property", "123 Main St", 1000, 200000, "user123", 3).Value));


            // Act
            var result = handler.Handle(validCommand, CancellationToken.None).Result;

            // Assert
            result.Success.Should().BeTrue();
            result.Listing.Should().NotBeNull();
            // Add additional assertions for the returned listing details

            // Verify that AddAsync was called once on the listingRepositoryMock
            listingRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Listing>()), Times.Once, "because a listing should be added");
        }

        [Fact]
        public void CreateListing_InvalidCommand_ShouldReturnValidationErrors()
        {
            // Arrange
            var listingRepositoryMock = new Mock<IListingRepository>();
            var userRepositoryMock = new Mock<IUserManager>();
            var propertyRepositoryMock = new Mock<IPropertyRepository>();

            var handler = new CreateListingCommandHandler(listingRepositoryMock.Object, userRepositoryMock.Object, propertyRepositoryMock.Object);

            var invalidCommand = new CreateListingCommand
            {
                // Populate the command with invalid data to trigger validation errors
            };

            // Act
            var result = handler.Handle(invalidCommand, CancellationToken.None).Result;

            // Assert
            result.Success.Should().BeFalse();
            result.ValidationsErrors.Should().NotBeEmpty();
            // Add additional assertions for the expected validation errors
            listingRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Listing>()), Times.Never);
        }

        [Fact]
        public void CreateListing_UserNotFound_ShouldReturnValidationError()
        {
            // Arrange
            var listingRepositoryMock = new Mock<IListingRepository>();
            var userRepositoryMock = new Mock<IUserManager>();
            var propertyRepositoryMock = new Mock<IPropertyRepository>();

            var handler = new CreateListingCommandHandler(listingRepositoryMock.Object, userRepositoryMock.Object, propertyRepositoryMock.Object);

            var commandWithNonExistingUser = new CreateListingCommand
            {
                Username = "nonExistingUsername",
                PropertyName = "Sample Property"
                // Add other necessary properties
            };

            userRepositoryMock.Setup(repo => repo.FindByUsernameAsync(It.IsAny<string>()))
                .ReturnsAsync(Result<UserDto>.Failure("User with the specified username does not exist."));

            // Act
            var result = handler.Handle(commandWithNonExistingUser, CancellationToken.None).Result;

            // Assert
            result.Success.Should().BeFalse();
            result.ValidationsErrors.Should().Contain("Title is required.");

            listingRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Listing>()), Times.Never);
        }

        [Fact]
        public void CreateListing_PropertyNotFound_ShouldReturnValidationError()
        {
            // Arrange
            var listingRepositoryMock = new Mock<IListingRepository>();
            var userRepositoryMock = new Mock<IUserManager>();
            var propertyRepositoryMock = new Mock<IPropertyRepository>();

            var handler = new CreateListingCommandHandler(listingRepositoryMock.Object, userRepositoryMock.Object, propertyRepositoryMock.Object);

            var commandWithNonExistingProperty = new CreateListingCommand
            {
                Username = "user123",
                PropertyName = "nonExistingProperty"
                // Add other necessary properties
            };

            userRepositoryMock.Setup(repo => repo.FindByUsernameAsync(It.IsAny<string>()))
                .ReturnsAsync(Result<UserDto>.Success(new UserDto()));

            propertyRepositoryMock.Setup(repo => repo.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(Result<Property>.Failure("Property with the specified name does not exist."));

            // Act
            var result = handler.Handle(commandWithNonExistingProperty, CancellationToken.None).Result;

            // Assert
            result.Success.Should().BeFalse();
            result.ValidationsErrors.Should().Contain("Title is required.");

            listingRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Listing>()), Times.Never);
        }
        [Fact]
        public void WhenCreateListingIsCalled_And_AllParametersAreValid_Then_SuccesIsReturned()
        {
            // Arrange
            string title = "Test Title";
            decimal price = 100.00m;
            string username = "TestUser";
            string propertyName = "Test Property";
            string description = "Test Description";
            Status propertyStatus = Status.ForRent;


            // Act
            var result = Listing.Create(title, price, username, propertyName, description, propertyStatus);

            // Assert
            Assert.True(result.IsSuccess);
        }
        [Fact]
        public void WhenCreateListingIsCalled_And_UsernameIsInvalid_Then_FailureIsReturned()
        {
            // Arrange
            string title = "Test Title";
            decimal price = 100.00m;
            string username = null;
            string propertyName = "Test Property";
            string description = "Test Description";
            Status propertyStatus = Status.ForRent;

            // Act
            var result = Listing.Create(title, price, username, propertyName, description, propertyStatus);

            // Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public void WhenCreateListingIsCalled_And_PropertyNameIsInvalid_Then_FailureIsReturned()
        {
            // Arrange
            string title = "Test Title";
            decimal price = 100.00m;
            string username = "TestUser";
            string propertyName = null;
            string description = "Test Description";
            Status propertyStatus = Status.ForRent;

            // Act
            var result = Listing.Create(title, price, username, propertyName, description, propertyStatus);

            // Assert
            Assert.False(result.IsSuccess);

        }


        [Fact]
        public void WhenCreateListingIsCalled_And_TitleIsInvalid_Then_FailureIsReturned()
        {
            // Arrange
            string title = null;
            decimal price = 100.00m;
            string username = "TestUser";
            string propertyName = "Test propertyName";
            string description = "Test Description";
            Status propertyStatus = Status.ForRent;

            // Act
            var result = Listing.Create(title, price, username, propertyName, description, propertyStatus);

            // Assert
            Assert.False(result.IsSuccess);

        }
        [Fact]
        public void WhenCreateListingIsCalled_And_DescrptionIsInvalid_Then_FailureIsReturned()
        {
            // Arrange
            string title = "Test title";
            decimal price = 100.00m;
            string username = "TestUser";
            string propertyName = "Test propertyName";
            string description = null;
            Status propertyStatus = Status.ForRent;

            // Act
            var result = Listing.Create(title, price, username, propertyName, description, propertyStatus);

            // Assert
            Assert.False(result.IsSuccess);

        }

        [Fact]
        public void WhenCreateListingIsCalled_And_PriceIsInvalid_Then_FailureIsReturned()
        {
            // Arrange
            string title = "Test title";
            decimal price = 0.00m;
            string username = "TestUser";
            string propertyName = "Test propertyName";
            string description = "Test description";
            Status propertyStatus = Status.ForRent;

            // Act
            var result = Listing.Create(title, price, username, propertyName, description, propertyStatus);

            // Assert
            Assert.False(result.IsSuccess);

        }
        [Fact]
        public void WhenUpdateTitleListingIsCalled_And_TitleIsValid_Then_UpdatesTitle()
        {
            string title = "Test Title";
            decimal price = 100.00m;
            string username = "TestUser";
            string propertyName = "Test Property";
            string description = "Test Description";
            Status propertyStatus = Status.ForRent;
            var listing = new Listing(title, price, username, propertyName, description, propertyStatus);
            string newTitle = "New Title";

            // Act
            listing.UpdateTitle(newTitle);

            // Assert
            Assert.Equal(newTitle, listing.Title);
        }

        [Fact]
        public void WhenUpdateTitleListingIsCalled_And_TitleIsInvalid_Then_ThrowsArgumentExceptio()
        {
            string title = "Test Title";
            decimal price = 100.00m;
            string username = "TestUser";
            string propertyName = "Test Property";
            string description = "Test Description";
            Status propertyStatus = Status.ForRent;
            var listing = new Listing(title, price, username, propertyName, description, propertyStatus);
            string newTitle = null;



            // Assert
            Assert.Throws<ArgumentException>(() => listing.UpdateTitle(newTitle));
        }

        [Fact]
        public void WhenUpdateDescriptionListingIsCalled_And_DescriptionIsValid_Then_UpdateDescription()
        {
            string title = "Test Title";
            decimal price = 100.00m;
            string username = "TestUser";
            string propertyName = "Test Property";
            string description = "Test Description";
            Status propertyStatus = Status.ForRent;
            var listing = new Listing(title, price, username, propertyName, description, propertyStatus);
            string newDescription = "New Description";

            // Act
            listing.UpdateDescription(newDescription);

            // Assert
            Assert.Equal(newDescription, listing.Description);
        }

        [Fact]
        public void WhenUpdateDescriptionListingIsCalled_And_DescriptionIsInvalid_Then_ThrowsArgumentException()
        {
            string title = "Test Title";
            decimal price = 100.00m;
            string username = "TestUser";
            string propertyName = "Test Property";
            string description = "Test Description";
            Status propertyStatus = Status.ForRent;
            var listing = new Listing(title, price, username, propertyName, description, propertyStatus);
            string newDescription = null;

            // Assert
            Assert.Throws<ArgumentException>(() => listing.UpdateDescription(newDescription));



        }
        [Fact]
        public void WhenUpdateStatusListingIsCalled_And_StatusIsValid_Then_UpdateSatus()
        {
            string title = "Test Title";
            decimal price = 100.00m;
            string username = "TestUser";
            string propertyName = "Test Property";
            string description = "Test Description";
            Status propertyStatus = Status.ForRent;
            var listing = new Listing(title, price, username, propertyName, description, propertyStatus);
            Status newStatus = Status.ForSale;

            // Act
            listing.UpdateStatus(newStatus);

            // Assert
            Assert.Equal(newStatus, listing.PropertyStatus);
        }

        [Fact]
        public void WhenUpdateStatusListingIsCalled_And_StatusIsInvalid_Then_ThrowsArgumentException()
        {
            string title = "Test Title";
            decimal price = 100.00m;
            string username = "TestUser";
            string propertyName = "Test Property";
            string description = "Test Description";
            Status propertyStatus = Status.ForRent;
            var listing = new Listing(title, price, username, propertyName, description, propertyStatus);
            Status newStatus = Status.Unknown;

            // Assert
            Assert.Throws<ArgumentException>(() => listing.UpdateStatus(newStatus));



        }
        [Fact]
        public void WhenUpdatePriceListingIsCalled_And_PriceIsValid_Then_UpdatePrice()
        {
            string title = "Test Title";
            decimal price = 100.00m;
            string username = "TestUser";
            string propertyName = "Test Property";
            string description = "Test Description";
            Status propertyStatus = Status.ForRent;
            var listing = new Listing(title, price, username, propertyName, description, propertyStatus);
            decimal newPrice = 50.00m;

            // Act
            listing.UpdatePrice(newPrice);

            // Assert
            Assert.Equal(newPrice, listing.Price);
        }

        [Fact]
        public void WhenUpdatePriceListingIsCalled_And_PriceIsInvalid_Then_ThrowsArgumentExceptio()
        {
            string title = "Test Title";
            decimal price = 100.00m;
            string username = "TestUser";
            string propertyName = "Test Property";
            string description = "Test Description";
            Status propertyStatus = Status.ForRent;
            var listing = new Listing(title, price, username, propertyName, description, propertyStatus);
            decimal newPrice = 0;



            // Assert
            Assert.Throws<ArgumentException>(() => listing.UpdatePrice(newPrice));
        }





    }



}
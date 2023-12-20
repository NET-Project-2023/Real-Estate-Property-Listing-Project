using Real_estate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Testing
{
    public class PropertyTests
    {
        [Fact]
        public void WhenCreatePropertyIsCalled_And_AllParametersAreValid_Then_SuccessIsReturned()
        {
            // Arrange
            string title = "Test Title";
            string address = "Test Address";
            int size = 100;
            int price = 100000;
            string ownerId = "Test OwnerId";
            int numberOfBedrooms = 3;

            // Act
            var result = Property.Create(title, address, size, price, ownerId, numberOfBedrooms);

            // Assert
            Assert.True(result.IsSuccess);
        }
        [Fact]
        public void WhenCreatePropertyIsCalled_And_TitleIsInvalid_Then_FailureIsReturned()
        {
            // Arrange
            string title = null;
            string address = "Test Address";
            int size = 100;
            int price = 100000;
            string ownerId = "Test OwnerId";
            int numberOfBedrooms = 2;

            // Act
            var result = Property.Create(title, address, size, price, ownerId, numberOfBedrooms);

            // Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public void WhenCreatePropertyIsCalled_And_AddressIsInvalid_Then_FailureIsReturned()
        {
            // Arrange
            string title = "Test Title";
            string address = null; // Invalid address
            int size = 100;
            int price = 100000;
            string ownerId = "Test OwnerId";
            int numberOfBedrooms = 3;

            // Act
            var result = Property.Create(title, address, size, price, ownerId, numberOfBedrooms);

            // Assert
            Assert.False(result.IsSuccess);
        }
        [Fact]
        public void WhenCreatePropertyIsCalled_And_SizeIsInvalid_Then_FailureIsReturned()
        {
            // Arrange
            string title = "Test Property";
            string address = "Test Address";
            int size = 0; // Invalid size
            int price = 100000;
            string ownerId = "Test OwnerId";
            int numberOfBedrooms = 2;

            // Act
            var result = Property.Create(title, address, size, price, ownerId, numberOfBedrooms);

            // Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public void WhenCreatePropertyIsCalled_And_PriceIsInvalid_Then_FailureIsReturned()
        {
            // Arrange
            string title = "Test Property";
            string address = "Test Address";
            int size = 100;
            int price = -100000; // Invalid price
            string ownerId = "Test OwnerId";

            int numberOfBedrooms = 2;

            // Act
            var result = Property.Create(title, address, size, price, ownerId, numberOfBedrooms);

            // Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public void WhenCreatePropertyIsCalled_And_OwnerIdIsInvalid_Then_FailureIsReturned()
        {
            // Arrange
            string title = "Test Property";
            string address = "Test Address";
            int size = 100;
            int price = 100000;
            string ownerId = null;

            int numberOfBedrooms = 2;

            // Act
            var result = Property.Create(title, address, size, price, ownerId, numberOfBedrooms);

            // Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public void WhenCreatePropertyIsCalled_And_NumberOfBedroomsIsInvalid_Then_FailureIsReturned()
        {
            // Arrange
            string title = "Test Property";
            string address = "Test Address";
            int size = 100;
            int price = 100000;
            string ownerId = "Test OwnerId";

            int numberOfBedrooms = -2; // Invalid numberOfBedrooms

            // Act
            var result = Property.Create(title, address, size, price, ownerId, numberOfBedrooms);

            // Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public void WhenAttachDescriptionIsCalled_And_DescriptionIsValid_Then_DescriptionIsAttached()
        {
            // Arrange
            string title = "Test Title";
            string address = "Test Address";
            int size = 100;
            int price = 100000;
            string ownerId = "Test OwnerId";

            int numberOfBedrooms = 3;

            var property = new Property(title, address, size, price, ownerId, numberOfBedrooms);

            string description = "Test Description";

            // Act
            property.AttachDescription(description);

            // Assert
            Assert.Equal(description, property.Description);
        }

        [Fact]
        public void WhenAttachDescriptionIsCalled_And_DescriptionIsInvalid_Then_DescriptionIsNotAttached()
        {
            // Arrange
            var property = new Property("Test Title", "Test Address", 100, 100000, "Test OwnerId", 3);
            string description = null; // Invalid description

            // Act
            property.AttachDescription(description);
            Assert.Throws<ArgumentException>(() => property.UpdateDescription(description));

            // Assert
            Assert.Null(property.Description);
        }

        [Fact]
        public void WhenAttachImageUrlsIsCalled_And_ImagesAreValid_Then_ImagesAreAttached()
        {
            // Arrange
            var property = new Property("Test Title", "Test Address", 100, 100000, "Test OwnerId", 3);
            List<byte[]> images = new List<byte[]> { new byte[] { 1, 2, 3 }, new byte[] { 4, 5, 6 } };

            // Act
            property.AttachImageUrls(images);

            // Assert
            Assert.Equal(images, property.Images);
        }

        [Fact]
        public void WhenAttachImageUrlsIsCalled_And_ImagesListIsNull_Then_ArgumentExceptionIsThrown()
        {
            // Arrange
            var property = new Property("Test Title", "Test Address", 100, 100000, "Test OwnerId", 3);
            List<byte[]> images = null; // Invalid images list

            // Act & Assert
            Assert.Throws<ArgumentException>(() => property.AttachImageUrls(images));
        }

        [Fact]
        public void WhenAttachImageUrlsIsCalled_And_ImagesListIsEmpty_Then_ArgumentExceptionIsThrown()
        {
            // Arrange
            var property = new Property("Test Title", "Test Address", 100, 100000, "Test OwnerId", 3);
            List<byte[]> images = new List<byte[]>(); // Invalid empty images list

            // Act & Assert

            Assert.Throws<ArgumentException>(() => property.AttachImageUrls(images));
        }
        [Fact]
        public void WhenAttachNumberOfBathroomsIsCalled_And_CurrentNumberOfBathroomsIsZero_Then_NumberOfBathroomsIsUpdated()
        {
            // Arrange
            var property = new Property("Test Title", "Test Address", 100, 100000, "Test OwnerId", 3);
            int initialNumberOfBathrooms = 0;
            int newNumberOfBathrooms = 2;

            // Act
            property.AttachNumberOfBathrooms(newNumberOfBathrooms);

            // Assert
            Assert.Equal(newNumberOfBathrooms, property.NumberOfBathrooms);
        }

        [Fact]
        public void WhenAttachNumberOfBathroomsIsCalled_And_CurrentNumberOfBathroomsIsNotZero_Then_NumberOfBathroomsIsNotUpdated()
        {
            // Arrange
            var property = new Property("Test Title", "Test Address", 100, 100000, "Test OwnerId", 3);
            int initialNumberOfBathrooms = 2; // Non-zero initial value
            int newNumberOfBathrooms = 3;

            // Set the initial value
            property.AttachNumberOfBathrooms(initialNumberOfBathrooms);

            // Act
            property.AttachNumberOfBathrooms(newNumberOfBathrooms);

            // Assert
            Assert.Equal(initialNumberOfBathrooms, property.NumberOfBathrooms);
        }


        [Fact]
        public void WhenUpdateDescriptionIsCalled_And_NewDescriptionIsValid_Then_DescriptionIsUpdated()
        {
            // Arrange
            var property = new Property("Test Title", "Test Address", 100, 100000, "Test OwnerId", 3);
            string newDescription = "New Description";

            // Act
            property.UpdateDescription(newDescription);

            // Assert
            Assert.Equal(newDescription, property.Description);
        }

        [Fact]
        public void WhenUpdateDescriptionIsCalled_And_NewDescriptionIsInvalid_Then_ArgumentExceptionIsThrown()
        {
            // Arrange
            var property = new Property("Test Title", "Test Address", 100, 100000, "Test OwnerId", 3);
            string newDescription = null; // Invalid new description

            // Act & Assert
            Assert.Throws<ArgumentException>(() => property.UpdateDescription(newDescription));
        }

        [Fact]
        public void WhenUpdateAddressIsCalled_And_NewAddressIsValid_Then_AddressIsUpdated()
        {
            // Arrange
            var property = new Property("Test Title", "Test Address", 100, 100000, "Test OwnerId", 3);
            string newAddress = "New Address";

            // Act
            property.UpdateAddress(newAddress);

            // Assert
            Assert.Equal(newAddress, property.Address);
        }

        [Fact]
        public void WhenUpdateAddressIsCalled_And_NewAddressIsInvalid_Then_ArgumentExceptionIsThrown()
        {
            // Arrange
            var property = new Property("Test Title", "Test Address", 100, 100000, "Test OwnerId", 3);
            string newAddress = null; // Invalid new address

            // Act & Assert
            Assert.Throws<ArgumentException>(() => property.UpdateAddress(newAddress));
        }

        [Fact]
        public void WhenUpdateSizeIsCalled_And_NewSizeIsValid_Then_SizeIsUpdated()
        {
            // Arrange
            var property = new Property("Test Title", "Test Address", 100, 100000, "Test OwnerId", 3);
            int newSize = 150;

            // Act
            property.UpdateSize(newSize);

            // Assert
            Assert.Equal(newSize, property.Size);
        }

        [Fact]
        public void WhenUpdateSizeIsCalled_And_NewSizeIsInvalid_Then_ArgumentExceptionIsThrown()
        {
            // Arrange
            var property = new Property("Test Title", "Test Address", 100, 100000, "Test OwnerId", 3);
            int newSize = 0; // Invalid new size

            // Act & Assert
            Assert.Throws<ArgumentException>(() => property.UpdateSize(newSize));
        }

        [Fact]
        public void WhenUpdatePriceIsCalled_And_NewPriceIsValid_Then_PriceIsUpdated()
        {
            // Arrange
            var property = new Property("Test Title", "Test Address", 100, 100000, "Test OwnerId", 3);
            int newPrice = 120000;

            // Act
            property.UpdatePrice(newPrice);

            // Assert
            Assert.Equal(newPrice, property.Price);
        }

        [Fact]
        public void WhenUpdatePriceIsCalled_And_NewPriceIsNegative_Then_ArgumentExceptionIsThrown()
        {
            // Arrange  
            var property = new Property("Test Title", "Test Address", 100, 100000, "Test OwnerId", 3);
            int newPrice = -10000; // Invalid new price

            // Act & Assert
            Assert.Throws<ArgumentException>(() => property.UpdatePrice(newPrice));
        }
        [Fact]
        public void WhenUpdateNumberOfBedroomsIsCalled_And_NewNumberOfBedroomsIsValid_Then_NumberOfBedroomsIsUpdated()
        {
            // Arrange
            var property = new Property("Test Title", "Test Address", 100, 100000, "Test OwnerId", 3);
            int newNumberOfBedrooms = 4;

            // Act
            property.UpdateNumberOfBedrooms(newNumberOfBedrooms);

            // Assert
            Assert.Equal(newNumberOfBedrooms, property.NumberOfBedrooms);
        }

        [Fact]
        public void WhenUpdateNumberOfBedroomsIsCalled_And_NewNumberOfBedroomsIsNegative_Then_ArgumentExceptionIsThrown()
        {
            // Arrange
            var property = new Property("Test Title", "Test Address", 100, 100000, "Test OwnerId", 3);
            int newNumberOfBedrooms = -1; // Invalid new number of bedrooms

            // Act & Assert
            Assert.Throws<ArgumentException>(() => property.UpdateNumberOfBedrooms(newNumberOfBedrooms));
        }

        [Fact]
        public void WhenUpdateNumberOfBathroomsIsCalled_And_NewNumberOfBathroomsIsValid_Then_NumberOfBathroomsIsUpdated()
        {
            // Arrange
            var property = new Property("Test Title", "Test Address", 100, 100000, "Test OwnerId", 3);
            int newNumberOfBathrooms = 2;

            // Act
            property.UpdateNumberOfBathrooms(newNumberOfBathrooms);

            // Assert
            Assert.Equal(newNumberOfBathrooms, property.NumberOfBathrooms);
        }

        [Fact]
        public void WhenUpdateNumberOfBathroomsIsCalled_And_NewNumberOfBathroomsIsNegative_Then_ArgumentExceptionIsThrown()
        {
            // Arrange
            var property = new Property("Test Title", "Test Address", 100, 100000, "Test OwnerId", 3);
            int newNumberOfBathrooms = -1; // Invalid new number of bathrooms

            // Act & Assert
            Assert.Throws<ArgumentException>(() => property.UpdateNumberOfBathrooms(newNumberOfBathrooms));
        }

    }
}

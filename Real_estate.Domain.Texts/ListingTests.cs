using Real_estate.Domain.Entities;
using static Real_estate.Domain.Enums.Enums;
namespace Real_estate.Domain.Texts
{
    public class ListingTests
    {

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
            string title = null ;
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
            Status newStatus=Status.;

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
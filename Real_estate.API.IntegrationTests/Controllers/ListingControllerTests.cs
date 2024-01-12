using FluentAssertions;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Real_estate.API.IntegrationTests.Base;
using Real_estate.Application.Features.Listings.Commands.CreateListing;
using Real_estate.Application.Features.Listings.Commands.UpdateListing;
using Real_estate.Application.Features.Listings.Queries;
using Real_estate.Application.Features.Properties.Commands.UpdateProperty;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using static Real_estate.Domain.Enums.Enums;

namespace Real_estate.API.IntegrationTests.Controllers
{
    public class ListingControllerTests : BaseApplicationContextTests
    {
        private const string RequestUri = "/api/v1/Listings";

        [Fact]
        public async Task When_GetAllCategoriesQueryHandlerIsCalled_Then_Success()
        {
            string token = CreateToken(); // Aici ar trebui să generezi un token valid
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Act
            var response = await Client.GetAsync(RequestUri);
            response.EnsureSuccessStatusCode();

            // Assert

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ListingDto>>(responseString);


            result?.Count().Should().Be(4);
        }


        [Fact]
        public async Task When_PostCategoryCommandHandlerIsCalledWithRightParameters_Then_TheEntityCreatedShouldBeReturned()
        {
            // Arrange
            string token = CreateToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var category = new CreateListingCommand
            {
                Title = "New Listing",
                Username = "user1",
                PropertyName = "Property1",
                Description = "Description of property",
                Price = 100.00m,
                PropertyStatus = Status.ForSale
            };

            // Act
            var response = await Client.PostAsJsonAsync(RequestUri, category);
            response.EnsureSuccessStatusCode();

            // Assert
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ListingDto>(responseString);

            result.Should().NotBeNull();
            result.Title.Should().Be(category.Title);
            result.Username.Should().Be(category.Username);
            result.PropertyName.Should().Be(category.PropertyName);
            result.Price.Should().Be(category.Price);
            result.PropertyStatus.Should().Be(category.PropertyStatus);
            result.Description.Should().Be(category.Description);
        }
        public class ListingsControllerIntegrationTests : BaseApplicationContextTests
        {
            private const string UpdateRequestUri = "/listings/update/";

            [Fact]
            public async Task UpdateListing_ReturnsOkStatus()
            {
                string token = CreateToken();
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // Arrange
                var updateCommand = new UpdateListingCommand
                {
                    Title = "House",
                    Description = "testing description"
                   
                };

                // Act
                var response = await Client.PutAsJsonAsync($"/listings/update/{updateCommand.Title}", updateCommand);

                // Assert
                response.EnsureSuccessStatusCode();
            }

            [Fact]
            public async Task UpdateListing_ReturnsBadRequestWhenIdMismatch()
            {
                // Arrange
                var listingId = "House";
                var updateCommand = new UpdateListingCommand
                {
                    Title = "Updated House",
                    // Alte proprietăți actualizate
                };

                // Act
                var response = await Client.PutAsJsonAsync(UpdateRequestUri + listingId, updateCommand);

                // Assert
                response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeAnonymousType(responseString, new { Message = "" });

                result.Should().NotBeNull();
                result.Message.Should().Be("Listing ID mismatch.");
            }
        }

        private static string CreateToken()
        {

            return JwtTokenProvider.JwtSecurityTokenHandler.WriteToken(
            new JwtSecurityToken(
                JwtTokenProvider.Issuer,
                JwtTokenProvider.Issuer,
                new List<Claim> { new(ClaimTypes.Role, "User"), new("department", "Security") },
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: JwtTokenProvider.SigningCredentials
            ));
        }
    }
    public class ListingsControllerByTitleIntegrationTests : BaseApplicationContextTests
    {
        private const string RequestUri = "/api/v1/Listings/ByTitle/";

        [Fact]
        public async Task GetListingByTitle_ReturnsOkStatusAndCorrectResult()
        {
            // Arrange
            string titleToRetrieve = "House"; // Asigură-te că acest titlu există în baza de date de test

            // Act
            var response = await Client.GetAsync(RequestUri + titleToRetrieve);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ListingDto>(responseString);

            result.Should().NotBeNull();
            result.Title.Should().Be(titleToRetrieve);

        }

        [Fact]
        public async Task GetListingByNonexistentTitle_ReturnsNotFoundStatus()
        {
            // Arrange
            string nonexistentTitle = "casa"; // Asigură-te că acest titlu NU există în baza de date de test

            // Act
            var response = await Client.GetAsync(RequestUri + nonexistentTitle);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
        public class ListingsControllerIntegrationTests : BaseApplicationContextTests
        {
            private const string RequestUri = "/api/v1/Listings/listings/delete/";

            [Fact]
            public async Task DeleteListing_ReturnsOkStatus()
            {

                string titleToRetrieve = "House"; // Asigură-te că acest titlu există în baza de date de test

                // Act
                var responseT = await Client.GetAsync("/api/v1/Listings/ByTitle/" + titleToRetrieve);

                // Assert
                responseT.StatusCode.Should().Be(HttpStatusCode.OK);

                var responseString = await responseT.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ListingDto>(responseString);





                // Arrange
                Guid listingIdToDelete = result.ListingId;

                // Act
                var response = await Client.DeleteAsync(RequestUri + listingIdToDelete);

                // Assert
                response.StatusCode.Should().Be(HttpStatusCode.OK);

                // ... alte aserțiuni dacă sunt necesare


            }


            [Fact]
            public async Task DeleteNonexistentListing_ReturnsNotFoundStatus()
            {
                // Arrange
                Guid listingIdToDelete = Guid.Parse("870068c8-e16f-4e86-91ed-bbf67477bc83"); // Asigură-te că acest ID NU există în baza de date de test

                // Act
                var response = await Client.DeleteAsync(RequestUri + listingIdToDelete);

                // Assert
                response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }

    }
}

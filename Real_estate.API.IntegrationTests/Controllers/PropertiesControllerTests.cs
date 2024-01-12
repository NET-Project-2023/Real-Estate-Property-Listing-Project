using Newtonsoft.Json;
using Real_estate.API.IntegrationTests.Base;
using Real_estate.Application.Features.Properties.Commands.CreateProperty;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using Real_estate.Application.Features.Properties.Queries;
using System.Net.Http.Json;
using FluentAssertions;
using System.Net;
using Real_estate.Application.Features.Properties.Commands.UpdateProperty;

namespace Real_estate.API.IntegrationTests.Controllers
{
    public class PropertiesControllerTests: BaseApplicationContextTests
    {

        [Fact]
        public async Task When_ValidCreatePropertyCommandIsSent_Then_PropertyIsCreated()
        {
            var dummyImage = new byte[] { 0x20, 0x20, 0x20, 0x20 };

            // Arrange
            string token = CreateToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var createPropertyCommand = new CreatePropertyCommand
            {
                Title = "Test Property",
                Address = "123 Test Street",
                Size = 100,
                Price = 500000,
                UserId = "UserTest",
                NumberOfBedrooms = 3,
                NumberOfBathrooms = 4,
                Images = new List<byte[]> { dummyImage }
            }; 

            // Act
            var response = await Client.PostAsJsonAsync("/api/v1/Properties", createPropertyCommand);

            // Assert
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CreatePropertyDto>(responseString);

            result.Should().NotBeNull();
            result.Title.Should().Be(createPropertyCommand.Title);
            result.Address.Should().Be(createPropertyCommand.Address);
            result.Size.Should().Be(createPropertyCommand.Size);
            result.Price.Should().Be(createPropertyCommand.Price);
            result.UserId.Should().Be(createPropertyCommand.UserId);
            result.NumberOfBedrooms.Should().Be(createPropertyCommand.NumberOfBedrooms);

        }

        [Fact]
        public async Task When_GetAllCategoriesQueryHandlerIsCalled_Then_Success()
        {
            string token = CreateToken(); // Aici ar trebui să generezi un token valid
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Act
            var response = await Client.GetAsync("/api/v1/Properties");
            response.EnsureSuccessStatusCode();

            // Assert

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<PropertyDto>>(responseString);


            result?.Count().Should().Be(4);
        }

        [Fact]
        public async Task When_GetPropertyByNameWithValidName_Then_Success()
        {
            // Arrange
            string token = CreateToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string validPropertyName = "Villa";// Set a valid property name

            // Act
            var response = await Client.GetAsync($"/api/v1/Properties/ByName/{validPropertyName}");

            // Assert
            response.EnsureSuccessStatusCode();
            // Additional success assertions
        }

        [Fact]
        public async Task When_GetPropertyByNameWithInvalidName_Then_Failure()
        {
            // Arrange
            string token = CreateToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string invalidPropertyName = "masa"; 

            // Act
            var response = await Client.GetAsync($"/api/v1/Properties/ByName/{invalidPropertyName}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            // Additional failure assertions
        }

        [Fact]
        public async Task When_DeletePropertyWithValidId_Then_Success()
        {
            // Arrange
            string token = CreateToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string titleToRetrieve = "House";
            // Act
            var responseT = await Client.GetAsync("/api/v1/Properties/ByName/" + titleToRetrieve);
            responseT.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseString = await responseT.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PropertyDto>(responseString);

            // Assert

            Guid propertyIdToDelete = result.PropertyId;

            // Act
            var response = await Client.DeleteAsync($"/api/v1/Properties/{propertyIdToDelete}");

            // Assert
            response.EnsureSuccessStatusCode();
            // Additional success assertions
        }

        [Fact]
        public async Task When_DeletePropertyWithInvalidId_Then_Failure()
        {
            // Arrange
            string token = CreateToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string invalidPropertyId = "258aec15-2c0c-4565-b4a9-553734df6eb7";// Set an invalid property ID

            // Act
            var response = await Client.DeleteAsync($"/api/v1/Properties/{invalidPropertyId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            // Additional failure assertions
        }

        [Fact]
        public async Task When_UpdatePropertyWithValidData_Then_Success()
        {
            // Arrange
            string token = CreateToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var updateCommand = new UpdatePropertyCommand
            {
                Title = "Villa",
                Address = "124 Test Street"
            };

            // Act
            var response = await Client.PutAsJsonAsync($"/api/v1/Properties/update/{updateCommand.Title}", updateCommand);

            // Assert
            response.EnsureSuccessStatusCode();
            // Additional success assertions
        }

        [Fact]
        public async Task When_UpdatePropertyWithInvalidData_Then_Failure()
        {
            // Arrange
            string token = CreateToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var updateCommand = new UpdatePropertyCommand
            {
                Title = "Test Updated"
            };

            // Act
            var response = await Client.PutAsJsonAsync($"/api/v1/Properties/update/{updateCommand.Title}", updateCommand);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            // Additional failure assertions
        }

        [Fact]
        public async Task When_GetPropertiesByCurrentUserWithValidUsername_Then_Success()
        {
            // Arrange
            string token = CreateToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string validUsername = "carina";// Set a valid username

            // Act
            var response = await Client.GetAsync($"/api/v1/Properties/ByCurrentUser/{validUsername}");

            // Assert
            response.EnsureSuccessStatusCode();
            // Additional success assertions
        }
/*
        [Fact]
        public async Task When_GetPropertiesByCurrentUserWithInvalidUsername_Then_Failure()
        {
            // Arrange
            string token = CreateToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string validUsername = "marina";// Set a invalid username

            // Act
            var response = await Client.GetAsync($"/api/v1/Properties/ByCurrentUser/{validUsername}");

            // Assert
            response.EnsureSuccessStatusCode();
            // Additional success assertions
        }
*/
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
}

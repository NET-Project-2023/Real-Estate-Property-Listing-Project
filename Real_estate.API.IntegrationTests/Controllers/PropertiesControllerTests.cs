using Newtonsoft.Json;
using Real_estate.API.IntegrationTests.Base;
using Real_estate.Application.Features.Properties.Commands.CreateProperty;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using Real_estate.Application.Features.Properties.Queries;
using System.Net.Http.Json;
using FluentAssertions;

namespace Real_estate.API.IntegrationTests.Controllers
{
    public class PropertiesControllerTests : BaseApplicationContextTests
    {
       
        
            private const string RequestUri = "/api/v1/Properties";

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
                var result = JsonConvert.DeserializeObject<List<PropertyDto>>(responseString);


                result?.Count().Should().Be(0);
            }


            [Fact]
            public async Task When_PostCategoryCommandHandlerIsCalledWithRightParameters_Then_TheEntityCreatedShouldBeReturned()
            {
                // Arrange
                string token = CreateToken();
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

         
                 var category = new CreatePropertyCommand
                {
                    Title = "New Property",
                    Address = "Address",
                    Size= 100,
                    Price= 200,
                    NumberOfBathrooms= 2,
                    Images= new List<byte[]>(),
                    NumberOfBedrooms= 2,
                    UserId="user1"
                   
                };

                // Act
                var response = await Client.PostAsJsonAsync(RequestUri, category);
                response.EnsureSuccessStatusCode();

                // Assert
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<PropertyDto>(responseString);

                result.Should().NotBeNull();
                result.Title.Should().Be(category.Title);
                result.Address.Should().Be(category.Address);
                result.Size.Should().Be(category.Size);
                result.Price.Should().Be(category.Price);
                result.NumberOfBedrooms.Should().Be(category.NumberOfBedrooms);
                result.NumberOfBathrooms.Should().Be(category.NumberOfBedrooms);

               
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
    }
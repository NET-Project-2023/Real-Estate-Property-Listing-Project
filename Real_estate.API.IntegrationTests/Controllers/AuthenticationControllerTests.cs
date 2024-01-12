using FluentAssertions;
using Newtonsoft.Json;
using Real_estate.Application.Models.Identity;

using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;
using Real_estate.API.IntegrationTests.Base;
using RealEstate.API.Models;
using System.Net.Http.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Real_estate.API.IntegrationTests.Controllers
{
    public class AuthenticationControllerTests : BaseApplicationContextTests
    {
        private const string AuthUri = "/api/v1/Authentication";

        [Fact]
        public async Task Login_WithCorrectCredentials_ReturnsOk()
        { // Arrange
            var loginModel = new LoginModel
            {
                Username = "BlueSkyWalker",
                Password = "DefaultP@ssword123!"
            };

            // Act
            var response = await Client.PostAsJsonAsync($"{AuthUri}/login", loginModel);

            // Check the response content if not successful
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new Exception($"API responded with {response.StatusCode}: {content}");
            }

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Login_WithIncorrectCredentials_ReturnsBadRequest()
        {
            // Arrange
            var loginModel = new LoginModel { 
                Username = "user", Password = "parola123" 
            };

            // Act
            var response = await Client.PostAsJsonAsync($"{AuthUri}/login", loginModel);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Register_WithCorrectData_ReturnsCreatedAtAction()
        {
            // Arrange
            var registrationModel = new RegistrationModel {

                Username = "NewUser123",
                Name = "John Doe",
                Email = "johndoe@example.com",
                Password = "StrongP@ssw0rd!",
                ConfirmedPassword = "StrongP@ssw0rd!"
            };

            // Act
            var response = await Client.PostAsJsonAsync($"{AuthUri}/register", registrationModel);
            response.EnsureSuccessStatusCode();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task Register_WithIncorrectData_ReturnsBadRequest()
        {
            // Arrange
            var registrationModel = new RegistrationModel { /* Setează datele de înregistrare incorecte */ };

            // Act
            var response = await Client.PostAsJsonAsync($"{AuthUri}/register", registrationModel);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Logout_ReturnsOk()
        {
            // Act
            var response = await Client.PostAsync($"{AuthUri}/logout", null);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task CurrentUserInfo_WhenUserIsAuthenticated_ReturnsUserInfo()
        {
            // Arrange
            string token = CreateToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Act
            var response = await Client.GetAsync($"{AuthUri}/currentuserinfo");
            response.EnsureSuccessStatusCode();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CurrentUser>(responseString);

            result.Should().NotBeNull();
            result.IsAuthenticated.Should().BeTrue();
            // Adaugă mai multe aserțiuni în funcție de informațiile specifice așteptate pentru utilizatorul curent.
        }

        [Fact]
        public async Task CurrentUserInfo_WhenUserIsNotAuthenticated_ReturnsNotAuthenticatedUserInfo()
        {
            // Act
            var response = await Client.GetAsync($"{AuthUri}/currentuserinfo");
            response.EnsureSuccessStatusCode();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CurrentUser>(responseString);

            result.Should().NotBeNull();
            result.IsAuthenticated.Should().BeFalse();
            // Alte aserțiuni dacă sunt necesare pentru utilizatorul neautentificat.
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
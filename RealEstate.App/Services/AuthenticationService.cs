using RealEstate.App.Contracts;
using RealEstate.App.ViewModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace RealEstate.App.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public AuthenticationService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }

        public async Task Login(LoginViewModel loginRequest)
        {
            var response = await httpClient.PostAsJsonAsync("api/v1/authentication/login", loginRequest);
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
            response.EnsureSuccessStatusCode();
            var token = await response.Content.ReadAsStringAsync();
            await tokenService.SetTokenAsync(token);
        }

        public async Task Logout()
        {
            await tokenService.RemoveTokenAsync();
            var result = await httpClient.PostAsync("api/v1/authentication/logout", null);
            result.EnsureSuccessStatusCode();
        }

        public async Task Register(RegisterViewModel registerRequest)
        {
            var result = await httpClient.PostAsJsonAsync("api/v1/authentication/register", registerRequest);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                throw new Exception(await result.Content.ReadAsStringAsync());
            }
            result.EnsureSuccessStatusCode();
        }

        public async Task DeleteUserByUsername(string username)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"api/v1/users/deleteByUsername/{username}");

            // Add the bearer token to the request
            var token = await tokenService.GetTokenAsync();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.SendAsync(request);

            Console.WriteLine("Numele username-ului: " + username);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new Exception("User not found");
            }
            response.EnsureSuccessStatusCode();
        }

        public Task<string> DecodeUsernameFromTokenAsync(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(token);
            var username = jsonToken.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value;
            return Task.FromResult(username)!;
        }

    }
}

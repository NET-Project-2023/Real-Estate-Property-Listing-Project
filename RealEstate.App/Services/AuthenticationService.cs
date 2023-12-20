using RealEstate.App.Contracts;
using RealEstate.App.ViewModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;

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
        public async Task UpdateUser(UpdateUserViewModel updateUserModel)
        {
            var requestUri = $"api/v1/users/update/{updateUserModel.Username}"; // URL should match the backend route parameter

            Console.WriteLine("Preparing to update user...");

            // Serialize the updateUserModel to JSON and print it to the console for debugging
            var jsonContent = JsonSerializer.Serialize(updateUserModel);
            Console.WriteLine($"JSON Payload: {jsonContent}");

            // Check if name is null or empty
            if (string.IsNullOrEmpty(updateUserModel.Name))
            {
                Console.WriteLine("The 'Name' field is empty or null. Update operation aborted.");
                return;
            }

            var request = new HttpRequestMessage(HttpMethod.Put, requestUri);

            // Add the bearer token to the request
            var token = await tokenService.GetTokenAsync();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Add the JSON content to the request
            request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Send the request to update the user
            var response = await httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error updating user: {errorContent}");
                throw new ApplicationException($"Error updating user: {errorContent}");
            }
            else
            {
                Console.WriteLine("User updated successfully.");
            }
        }

    }
}

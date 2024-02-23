using RealEstate.App.Contracts;
using RealEstate.App.Services.Responses;
using RealEstate.App.ViewModels;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Net.Http.Json;


namespace RealEstate.App.Services
{
    public class UserService : IUserService
    {
        private const string RequestUri = "api/v1/Users";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;
        public UserService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }

        public async Task<UserViewModel> GetUser(string username)
        {
            try
            {
                if (!string.IsNullOrEmpty(username))
                {
                    Console.WriteLine($"Print din GetUser SERVICE: {username}");
                }
                else
                {
                    Console.WriteLine("Suntem aici dar nuj cum");
                }
                var requestUri = $"api/v1/users/ByName/{username}";
                httpClient.DefaultRequestHeaders.Authorization =
                   new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

                var response = await httpClient.GetAsync(requestUri);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                using var doc = JsonDocument.Parse(content);
                var userElement = doc.RootElement.GetProperty("user");

                var user = JsonSerializer.Deserialize<UserViewModel>(userElement.GetRawText(), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return user!;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during deserialization: {ex}");
                throw;
            }
            
        }

        public async Task<ApiResponse<UserViewModel>> UpdateUser(UserViewModel updateUserModel)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

            var requestUri = $"api/v1/Users/update/{updateUserModel.Username}";

            Console.WriteLine($"Preparing to update user phoneNumber... {updateUserModel.PhoneNumber}");

            var result = await httpClient.PutAsJsonAsync(requestUri, updateUserModel);
            result.EnsureSuccessStatusCode();
            var response = await result.Content.ReadFromJsonAsync<ApiResponse<UserViewModel>>();
            response!.IsSuccess = result.IsSuccessStatusCode;
            return response!;
        }


        public async Task<List<UserViewModel>> GetAllUsers()
        {
            var requestUri = "api/v1/Users";
            httpClient.DefaultRequestHeaders.Authorization =
               new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

            var result = await httpClient.GetAsync(requestUri);
            result.EnsureSuccessStatusCode();

            var content = await result.Content.ReadAsStringAsync();

            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var response = JsonSerializer.Deserialize<UsersResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            var users = response?.Users ?? new List<UserViewModel>();

            return users!;
        }

    }

}

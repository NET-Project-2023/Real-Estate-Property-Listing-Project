using RealEstate.App.Contracts;
using RealEstate.App.Services.Responses;
using RealEstate.App.ViewModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;


namespace RealEstate.App.Services
{
    public class PropertyDataService : IPropertyDataService
    {
        private const string RequestUri = "api/v1/properties";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public PropertyDataService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }

        public async Task<ApiResponse<PropertyDto>> CreatePropertyAsync(PropertyViewModel propertyViewModel)
        {
            try
            {
                string loggedInUser = await tokenService.GetUsernameFromTokenAsync();
                if (string.IsNullOrEmpty(loggedInUser))
                {
                    throw new InvalidOperationException("Logged-in username not available.");
                }
                Console.WriteLine($"Logged-in User ID: {loggedInUser}");


                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

                propertyViewModel.UserId = loggedInUser;
                
                // TODO: adauga imagini reale!
                propertyViewModel.Images = new List<byte[]> { new byte[0] };

                var result = await httpClient.PostAsJsonAsync(RequestUri, propertyViewModel);
                result.EnsureSuccessStatusCode();
                    

                var responseContent = await result.Content.ReadAsStringAsync();
                Console.WriteLine($"API Response: {responseContent}");

                var response = await result.Content.ReadFromJsonAsync<ApiResponse<PropertyDto>>();
                response!.IsSuccess = result.IsSuccessStatusCode;
                return response!;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP Request Exception: {ex.Message}");
                throw; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected exception occurred: {ex.Message}");
                throw;
            }
        }
        public async Task<List<PropertyViewModel>> GetPropertiesByCurrentUserAsync()
        {
            try
            {
                string loggedInUserId = await tokenService.GetUsernameFromTokenAsync();
                if (string.IsNullOrEmpty(loggedInUserId))
                {
                    throw new InvalidOperationException("Logged-in username not available.");
                }
                Console.WriteLine($"Logged-in User ID from Blazor: {loggedInUserId}");


                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
                
                // Trebuie sa cautam toate Properties cu UserId==loggedInUserId

                var result = await httpClient.GetAsync($"api/v1/Properties/ByCurrentUser/{loggedInUserId}", HttpCompletionOption.ResponseHeadersRead);

                result.EnsureSuccessStatusCode();

                var content = await result.Content.ReadAsStringAsync();
                Console.WriteLine($"Bad Content from BLAZOR: {content}");

                if (!result.IsSuccessStatusCode)
                {
                    throw new ApplicationException(content);
                }

                // Aici e problema!
                var response = JsonSerializer.Deserialize<PropertiesResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                var properties = response?.Properties ?? new List<PropertyViewModel>();

                return properties!;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP Request Exception: {ex.Message}");
                Console.WriteLine($"Request URL: {httpClient.BaseAddress}/{RequestUri}/ByOwner/");
                throw; // Rethrow the exception after logging
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected exception occurred: {ex.Message}");
                throw;
            }
        }
        public async Task<List<PropertyViewModel>> GetPropertiesAsync()
        {
            // Ensure the token is included in the request headers
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

            var result = await httpClient.GetAsync(RequestUri, HttpCompletionOption.ResponseHeadersRead);
            result.EnsureSuccessStatusCode();

            var content = await result.Content.ReadAsStringAsync();
            Console.WriteLine($"Good Content from BLAZOR: {content}");

            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var response = JsonSerializer.Deserialize<PropertiesResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            var properties = response?.Properties ?? new List<PropertyViewModel>();


            return properties!;
        }
        public async Task<PropertyDto> GetPropertyByNameAsync(string propertyName)
       {
            
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

            var response = await httpClient.GetAsync($"{RequestUri}/ByName/{propertyName}");
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error fetching property: {response.ReasonPhrase}");
            }

            var propertyDto = await response.Content.ReadFromJsonAsync<PropertyDto>();
            return propertyDto ?? new PropertyDto();
       }
        public async Task<ApiResponse<PropertyDto>> UpdatePropertyAsync(PropertyDto propertyDto)
        {
            //string jsonPayload = JsonSerializer.Serialize(propertyDto);
            //Console.WriteLine($"Sending JSON Payload: {jsonPayload}");

            var token = await tokenService.GetTokenAsync();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string requestUri = $"api/v1/Properties/update/{propertyDto.Title}";
            propertyDto.Images = new List<byte[]> { new byte[0] };

            var response = await httpClient.PutAsJsonAsync(requestUri, propertyDto);

            // Aici primesc response asa: "Property updated successfully."
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response Content: {responseContent}");

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException($"Error updating listing: {responseContent}");
            }

            try
            {
                // Attempt to deserialize the response content to your ApiResponse object
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<PropertyDto>>(responseContent);
                apiResponse!.IsSuccess = response.IsSuccessStatusCode;
                return apiResponse;
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"JSON Deserialization Exception: {jsonEx.Message}");
                throw;
            }
        }
        public async Task<List<PropertyViewModel>> GetPropertyByIdAsync(Guid propertyId)
        {
            // Ensure the token is included in the request headers
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

            var result = await httpClient.GetAsync($"{RequestUri}/{propertyId}", HttpCompletionOption.ResponseHeadersRead);
            result.EnsureSuccessStatusCode();

            var content = await result.Content.ReadAsStringAsync();

            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var response = JsonSerializer.Deserialize<PropertiesResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var properties = response?.Properties ?? new List<PropertyViewModel>();
            return properties!;
        }


    }
}

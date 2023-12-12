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

        //public async Task<ApiResponse<PropertyDto>> CreatePropertyAsync(PropertyViewModel propertyViewModel)
        //{
        //    httpClient.DefaultRequestHeaders.Authorization
        //        = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
        //    var result = await httpClient.PostAsJsonAsync(RequestUri, propertyViewModel);
        //    result.EnsureSuccessStatusCode();
        //    var response = await result.Content.ReadFromJsonAsync<ApiResponse<PropertyDto>>();
        //    response!.IsSuccess = result.IsSuccessStatusCode;
        //    return response!;
        //}
        public async Task<ApiResponse<PropertyDto>> CreatePropertyAsync(PropertyViewModel propertyViewModel)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

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

            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var properties = JsonSerializer.Deserialize<List<PropertyViewModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });


            return properties!;
        }
    }
}

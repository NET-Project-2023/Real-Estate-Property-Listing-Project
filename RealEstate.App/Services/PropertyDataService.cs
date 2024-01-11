using Real_estate.Application.Features.Users.Queries.GetById;
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
        private const string RequestUri = "api/v1/Properties";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;
        private const long maxFileSize = 10485760; // 10 MB


        public PropertyDataService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }
        public async Task<ApiResponse<PropertyDto>> CreatePropertyAsync(PropertyViewModel propertyViewModel)
        {
            try
            {
                string loggedInUsername = await tokenService.GetUsernameFromTokenAsync();
                if (string.IsNullOrEmpty(loggedInUsername))
                {
                    throw new InvalidOperationException("Logged-in username not available.");
                }

                // Fetch user details by username
                var userResponse = await httpClient.GetAsync($"api/v1/Users/ByName/{loggedInUsername}");

                // Log raw response content
                var rawResponseContent = await userResponse.Content.ReadAsStringAsync();
                Console.WriteLine($"Raw API Response: {rawResponseContent}");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

                propertyViewModel.UserId = loggedInUsername;


                using var formContent = new MultipartFormDataContent();

                formContent.Add(new StringContent(propertyViewModel.Title ?? string.Empty), "Title");
                formContent.Add(new StringContent(propertyViewModel.Address ?? string.Empty), "Address");
                formContent.Add(new StringContent(propertyViewModel.Size.ToString()), "Size");
                formContent.Add(new StringContent(propertyViewModel.Price.ToString()), "Price");
                formContent.Add(new StringContent(propertyViewModel.NumberOfBedrooms.ToString()), "NumberOfBedrooms");
                formContent.Add(new StringContent(propertyViewModel.NumberOfBathrooms.ToString()), "NumberOfBathrooms");
                formContent.Add(new StringContent(propertyViewModel.UserId), "UserId");

                // Add files to the form content
                foreach (var browserFile in propertyViewModel.ImagesFiles)
                {
                    var fileContent = new StreamContent(browserFile.OpenReadStream(maxFileSize));
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(browserFile.ContentType);
                    formContent.Add(fileContent, "ImagesFiles", browserFile.Name);

                }
                // Send POST request with form content
                var response = await httpClient.PostAsync(RequestUri, formContent);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error Response: {errorContent}");
                    return new ApiResponse<PropertyDto> { IsSuccess = false, Message = errorContent };
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<PropertyDto>>(responseContent);
                return apiResponse ?? new ApiResponse<PropertyDto> { IsSuccess = false };

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
            using var formContent = new MultipartFormDataContent();

            formContent.Add(new StringContent(propertyDto.Title ?? string.Empty), "Title");
            formContent.Add(new StringContent(propertyDto.Address ?? string.Empty), "Address");
            formContent.Add(new StringContent(propertyDto.Size.ToString()), "Size");
            formContent.Add(new StringContent(propertyDto.Price.ToString()), "Price");
            formContent.Add(new StringContent(propertyDto.NumberOfBedrooms.ToString()), "NumberOfBedrooms");
            formContent.Add(new StringContent(propertyDto.NumberOfBathrooms.ToString()), "NumberOfBathrooms");
            formContent.Add(new StringContent(propertyDto.UserId), "UserId");

            // Add files to the form content
            foreach (var browserFile in propertyDto.ImagesFiles)
            {
                var fileContent = new StreamContent(browserFile.OpenReadStream(maxFileSize));
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(browserFile.ContentType);
                formContent.Add(fileContent, "ImagesFiles", browserFile.Name);

            }

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

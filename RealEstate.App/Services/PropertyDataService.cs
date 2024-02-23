using RealEstate.App.Contracts;
using RealEstate.App.Services.Responses;
using RealEstate.App.ViewModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using PropertyDto = RealEstate.App.ViewModels.PropertyDto;


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


                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

                propertyViewModel.UserId = loggedInUsername;


                using var formContent = new MultipartFormDataContent();

                formContent.Add(new StringContent(propertyViewModel.Title ?? string.Empty), "Title");
                formContent.Add(new StringContent(propertyViewModel.City ?? string.Empty), "City");
                formContent.Add(new StringContent(propertyViewModel.Description ?? string.Empty), "Description");
                formContent.Add(new StringContent(propertyViewModel.StreetAddress ?? string.Empty), "StreetAddress");
                formContent.Add(new StringContent(propertyViewModel.Size.ToString()), "Size");
                formContent.Add(new StringContent(propertyViewModel.NumberOfBedrooms.ToString()), "NumberOfBedrooms");
                formContent.Add(new StringContent(propertyViewModel.NumberOfBathrooms.ToString()), "NumberOfBathrooms");
                formContent.Add(new StringContent(propertyViewModel.UserId), "UserId");
                formContent.Add(new StringContent(""), "Images");

                if (propertyViewModel.ImagesFiles != null)
                {
                    foreach (var browserFile in propertyViewModel.ImagesFiles)
                    {
                        var fileContent = new StreamContent(browserFile.OpenReadStream(maxFileSize));
                        fileContent.Headers.ContentType = new MediaTypeHeaderValue(browserFile.ContentType);
                        formContent.Add(fileContent, "ImagesFiles", browserFile.Name);
                    }
                }
                else
                {
                    // Error here!!!
                    formContent.Add(new StringContent(""), "ImagesFiles");
                }


                // Send POST request with form content
                var response = await httpClient.PostAsync(RequestUri, formContent);
                response.EnsureSuccessStatusCode();

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();

                    return new ApiResponse<PropertyDto>
                    {
                        IsSuccess = false,
                        Message = "There was an error creating the listing.",
                        ValidationErrors = errorContent
                    };
                }

                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<PropertyDto>>();

                if (apiResponse == null)
                {
                    // Log the response content for debugging
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"API Response Content: {responseContent}");

                    throw new InvalidOperationException("Failed to deserialize API response");
                }
                else
                {
                    Console.WriteLine("API Response Data:");
                    Console.WriteLine($"PropertyId: {apiResponse.Data?.PropertyId}");
                    Console.WriteLine($"Title: {apiResponse.Data?.Title}");
                }

                apiResponse!.IsSuccess = response.IsSuccessStatusCode;

                return apiResponse!;

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
        public async Task<ApiResponse<PropertyDto>> UpdatePropertyAsync(PropertyViewModel propertyDto)
        {

            var token = await tokenService.GetTokenAsync();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string requestUri = $"api/v1/Properties/update/{propertyDto.Title}";
            using var formContent = new MultipartFormDataContent();

            formContent.Add(new StringContent(propertyDto.Title ?? string.Empty), "Title");
            formContent.Add(new StringContent(propertyDto.City ?? string.Empty), "City");
            formContent.Add(new StringContent(propertyDto.Description ?? string.Empty), "Description");
            formContent.Add(new StringContent(propertyDto.StreetAddress ?? string.Empty), "StreetAddress");
            formContent.Add(new StringContent(propertyDto.Size.ToString()), "Size");
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

            // Log the content being sent to the backend API
            var debugContent = await formContent.ReadAsStringAsync();
            Console.WriteLine($"REQUEST CONTENTTT: {debugContent}");

            var response = await httpClient.PutAsync(requestUri, formContent);
            
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response Content: {responseContent}");

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException($"Error updating property: {responseContent}");
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
        public async Task<PropertyViewModel> GetPropertyByIdAsync(Guid propertyId)
        {
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

            var result = await httpClient.GetAsync($"{RequestUri}/ById/{propertyId}", HttpCompletionOption.ResponseHeadersRead);
            result.EnsureSuccessStatusCode();

            var content = await result.Content.ReadAsStringAsync();

            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var property = JsonSerializer.Deserialize<PropertyViewModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return property;
        }

        public async Task<string> DeletePropertyAsync(Guid propertyId)
        {
            var response = await httpClient.DeleteAsync($"api/v1/Properties/{propertyId}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                // Handle error response if needed
                throw new HttpRequestException($"Failed to delete property. Status code: {response.StatusCode}");
            }
        }

    }
}

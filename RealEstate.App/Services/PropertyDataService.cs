﻿using RealEstate.App.Contracts;
using RealEstate.App.Services.Responses;
using RealEstate.App.ViewModels;
using System.Diagnostics;
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
                Console.WriteLine("HELLOOOOOOOOOOOOOOOOOOOOOOOO");
                string loggedInUserId = await tokenService.GetUsernameFromTokenAsync();
                if (string.IsNullOrEmpty(loggedInUserId))
                {
                    throw new InvalidOperationException("Logged-in username not available.");
                }
                Console.WriteLine($"Logged-in User ID: {loggedInUserId}");


                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

                propertyViewModel.UserId = loggedInUserId;
               
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

            var response = JsonSerializer.Deserialize<PropertiesResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            var properties = response?.Properties ?? new List<PropertyViewModel>();


            return properties!;
        }

        public async Task<PropertyViewModel> GetPropertyByIdAsync(Guid propertyId)
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

            var property = response?.Property ?? new PropertyViewModel();
            return property!;
        }
        public async Task<PropertyViewModel> GetPropertyByNameAsync(string propertyName)
        {
            // Ensure the token is included in the request headers
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

            var response = await httpClient.GetAsync($"{RequestUri}/ByName/{propertyName}");
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error fetching property: {response.ReasonPhrase}");
            }

            var property = await response.Content.ReadFromJsonAsync<PropertyViewModel>();
            return property ?? new PropertyViewModel();
        }
    }
}

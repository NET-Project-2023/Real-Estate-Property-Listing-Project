﻿using RealEstate.App.Contracts;
using RealEstate.App.Services.Responses;
using RealEstate.App.ViewModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace RealEstate.App.Services
{
    public class ListingDataService : IListingDataService
    {
        private const string RequestUri = "api/v1/listings";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public ListingDataService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }
        public async Task<List<ListingViewModel>> GetListingsAsync()
        {
            httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

            var result = await httpClient.GetAsync(RequestUri, HttpCompletionOption.ResponseHeadersRead);
            result.EnsureSuccessStatusCode();

            var content = await result.Content.ReadAsStringAsync();

            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var response = JsonSerializer.Deserialize<ListingsResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            var listings = response?.Listings ?? new List<ListingViewModel>();


            return listings!;
        }
        public async Task<ApiResponse<ListingViewModel>> CreateListingAsync(ListingViewModel listingViewModel)
        {
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

            var response = await httpClient.PostAsJsonAsync(RequestUri, listingViewModel);
            response.EnsureSuccessStatusCode();
            Console.WriteLine($"RESPONSE TATI: {response.StatusCode}"); 
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<ListingViewModel>>();
                apiResponse!.IsSuccess = response.IsSuccessStatusCode;
                return apiResponse!;
            }
            else
            {
                
                var errorContent = await response.Content.ReadAsStringAsync();
                
                return new ApiResponse<ListingViewModel>
                {
                    IsSuccess = false,
                    Message = "There was an error creating the listing.",
                    ValidationErrors = errorContent
                };
            }
        }

        //public async Task<ApiResponse<Guid>> DeleteListingAsync(Guid id)
        //{
        //    throw new NotImplementedException();
        //    //try
        //    //{
        //    //    httpClient.DefaultRequestHeaders.Authorization =
        //    //        new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

        //    //    var deleteResult = await httpClient.DeleteAsync($"{RequestUri}/{id}");

        //    //    if (deleteResult.IsSuccessStatusCode)
        //    //    {
        //    //        var deletedListing = await deleteResult.Content.ReadFromJsonAsync<ListingViewModel>();
        //    //        return new ApiResponse<ListingViewModel> { Data = deletedListing, IsSuccess = true };
        //    //    }
        //    //    else
        //    //    {
        //    //        var errorResponse = await deleteResult.Content.ReadFromJsonAsync<ErrorResponse>();
        //    //        return new ApiResponse<ListingViewModel> { ErrorMessage = errorResponse?.Message ?? "An error occurred", IsSuccess = false };
        //    //    }
        //    //}
        //    //catch (HttpRequestException ex)
        //    //{
        //    //    Console.WriteLine($"HTTP Request Exception: {ex.Message}");
        //    //    throw; // Rethrow the exception after logging
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    Console.WriteLine($"An unexpected exception occurred: {ex.Message}");
        //    //    throw;
        //    //}

        //}

        public Task<ListingViewModel> GetListingByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }


        public Task<ApiResponse<ListingViewModel>> UpdateListingAsync(ListingViewModel listingViewModel)
        {
            throw new NotImplementedException();

        }
    }
}
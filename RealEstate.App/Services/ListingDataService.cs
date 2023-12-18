using RealEstate.App.Contracts;
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


        public Task<ListingViewModel> GetListingByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }


        public async Task<ApiResponse<ListingViewModel>> UpdateListingAsync(ListingViewModel updateListingViewModel, string listingTitle)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

            var response = await httpClient.PutAsJsonAsync($"api/v1/listings/update/{listingTitle}", updateListingViewModel);

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<ListingViewModel>>();
                apiResponse.IsSuccess = response.IsSuccessStatusCode;
                return apiResponse;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return new ApiResponse<ListingViewModel>
                {
                    IsSuccess = false,
                    Message = "There was an error updating the listing.",
                    ValidationErrors = errorContent
                };
            }
        }
        public async Task<ListingViewModel> GetListingByTitleAsync(string title)
        {
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

            // Assuming your controller is set up as 'api/v1/listings' and your GetById action is 'ByTitle/{title}'
            var response = await httpClient.GetAsync($"api/v1/listings/ByTitle/{title}");

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadFromJsonAsync<ListingViewModel>();
                return apiResponse;

   
            }
            else
            {
                // Log the error or throw an exception
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error getting listing by title: {errorContent}");
                throw new ApplicationException($"Error getting listing by title: {errorContent}");
            }
        }
    }
}

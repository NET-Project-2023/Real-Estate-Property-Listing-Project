using RealEstate.App.Services.Responses;

namespace RealEstate.App.Contracts
{
    public interface IListingDataService
    {
        Task<List<ListingViewModel>> GetListingsAsync();
        Task<ListingViewModel> GetListingByIdAsync(Guid id);
        Task<ApiResponse<Guid>> CreateListingAsync(ListingViewModel listingViewModel);
        Task<ApiResponse<Guid>> UpdateListingAsync(ListingViewModel listingViewModel);
        Task<ApiResponse<Guid>> DeleteListingAsync(Guid id);
    }
}

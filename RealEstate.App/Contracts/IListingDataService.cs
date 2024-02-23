using RealEstate.App.Services.Responses;
using RealEstate.App.ViewModels;

namespace RealEstate.App.Contracts
{
    public interface IListingDataService
    {
        Task<List<ListingViewModel>> GetListingsAsync();
        Task<ListingViewModel> GetListingByIdAsync(Guid id);
        Task<ApiResponse<ListingViewModel>> CreateListingAsync(ListingViewModel listingViewModel);
        Task<ApiResponse<ListingViewModel>> UpdateListingAsync(ListingViewModel listingViewModel, string Title);
        Task<ListingViewModel> GetListingByTitleAsync(string title);
        Task<bool> DeleteListingAsync(Guid id);
    }
}

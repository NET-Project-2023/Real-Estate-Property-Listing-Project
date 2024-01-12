using RealEstate.App.Contracts;
using RealEstate.App.ViewModels;

namespace RealEstate.App.Services.Responses
{
    public class ListingsResponse
    {
        public List<ListingViewModel> Listings { get; set; }
        public ListingViewModel Listing { get; set; }
    }
}

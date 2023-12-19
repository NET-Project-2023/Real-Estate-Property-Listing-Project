using Real_estate.Application.Responses;

namespace Real_estate.Application.Features.Listings.Commands.CreateListing
{
    public class CreateListingCommandResponse : BaseResponse
    {
        public CreateListingCommandResponse() : base()
        {
        }
        public CreateListingDto Listing { get; set; }
    }
}

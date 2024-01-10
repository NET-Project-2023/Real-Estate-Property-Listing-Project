using Real_estate.Application.Responses;

namespace Real_estate.Application.Features.Listings.Commands.UpdateListing
{
    public class UpdateListingCommandResponse : BaseResponse
    {
        public UpdateListingCommandResponse() : base()
        {
        }
        public string Message { get; set; }
    }
}

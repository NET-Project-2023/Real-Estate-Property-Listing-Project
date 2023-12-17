using MediatR;

namespace Real_estate.Application.Features.Listings.Commands.UpdateListing
{
    public class UpdateListingCommand : IRequest<UpdateListingCommandResponse>
    {
        public Guid ListingId { get; set; }
        public string? Title { get; set; } // Optional update
        public string? Description { get; set; } // Optional update
        public decimal? Price { get; set; } // Optional update
    }
}

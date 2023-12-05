using MediatR;

namespace Real_estate.Application.Features.Listings.Commands.DeleteListing
{
    public class DeleteListingCommand : IRequest<DeleteListingCommandResponse>
    {
        public Guid ListingId { get; set; }
    }
}

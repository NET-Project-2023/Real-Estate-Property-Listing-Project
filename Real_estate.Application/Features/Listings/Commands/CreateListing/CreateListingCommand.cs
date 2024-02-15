using MediatR;
using static Real_estate.Domain.Enums.Enums;

namespace Real_estate.Application.Features.Listings.Commands.CreateListing
{
    public class CreateListingCommand : IRequest<CreateListingCommandResponse>
    {
        public string Title { get; set; } = default!;
        public Guid PropertyId { get; set; } // Property associated with the listing
        public decimal Price { get; set; }
        public string Username { get; set; } // User who is creating the listing
        public Status PropertyStatus { get; set; }

    }
}

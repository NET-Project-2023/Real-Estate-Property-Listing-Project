using MediatR;
using static Real_estate.Domain.Enums.Enums;

namespace Real_estate.Application.Features.Listings.Commands.UpdateListing
{
    public class UpdateListingCommand : IRequest<UpdateListingCommandResponse>
    {
        public string? Title { get; set; } // Optional update
        public string? Description { get; set; } // Optional update
        public decimal? Price { get; set; } // Optional update
        public Status Status { get; set; } // Optional update
    }
}

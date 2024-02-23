using MediatR;
using static Real_estate.Domain.Enums.Enums;

namespace Real_estate.Application.Features.Listings.Commands.UpdateListing
{
    public class UpdateListingCommand : IRequest<UpdateListingCommandResponse>
    {
        public string Title { get; set; } = default!;
        public Guid PropertyId { get; set; } 
        public decimal Price { get; set; }
        public Status PropertyStatus { get; set; }
    }
}

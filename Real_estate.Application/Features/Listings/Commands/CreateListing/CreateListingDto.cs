using static Real_estate.Domain.Enums.Enums;

namespace Real_estate.Application.Features.Listings.Commands.CreateListing
{
    public class CreateListingDto
    {
        public Guid ListingId { get; set; }
        public string? Title { get; set; }
        public string Username { get; set; } 
        public string PropertyName { get; set; } 
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public Status PropertyStatus { get; set; }

    }
}

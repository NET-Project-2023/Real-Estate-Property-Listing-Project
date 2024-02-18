

using static Real_estate.Domain.Enums.Enums;

namespace Real_estate.Application.Features.Listings.Queries
{
    public class ListingDto
    {
        public Guid ListingId { get; set; }
        public string Title { get; set; } = default!;
        public Guid PropertyId { get; set; } 
        public decimal Price { get; set; }
        public string Username { get; set; } 
        public Status PropertyStatus { get; set; }
        public DateTime LastModifiedAt { get; set; }
    }
}


namespace Real_estate.Application.Features.Listings.Queries
{
    public class ListingDto
    {
        public Guid ListingId { get; set; }
        public string? Title { get; set; }
        public Guid UserId { get; set; }
        public Guid PropertyId { get; set; }
        public string? Description { get; set; }

    }
}
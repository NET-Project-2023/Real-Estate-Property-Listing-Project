using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Real_estate.Domain.Enums.Enums;

namespace Real_estate.Application.Features.Listings.Commands.CreateListing
{
    public class CreateListingDto
    {
        public Guid ListingId { get; set; }
        public string? Title { get; set; }
        public string UserId { get; set; } 
        public Guid PropertyId { get; set; } 
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public Status PropertyStatus { get; set; }

    }
}

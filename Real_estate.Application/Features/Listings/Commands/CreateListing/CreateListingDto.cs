using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_estate.Application.Features.Listings.Commands.CreateListing
{
    public class CreateListingDto
    {
        public Guid ListingId { get; set; }
        public string? Title { get; set; }
        public Guid UserId { get; set; } 
        public Guid PropertyId { get; set; } 
        public string? Description { get; set; }

    }
}

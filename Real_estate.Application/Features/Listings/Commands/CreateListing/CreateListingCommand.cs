using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_estate.Application.Features.Listings.Commands.CreateListing
{
    public class CreateListingCommand : IRequest<CreateListingCommandResponse>
    {
        public string Title { get; set; } = default!;
        public Guid UserId { get; set; } // User who is creating the listing
        public Guid PropertyId { get; set; } // Property associated with the listing
        public string Description { get; set; } = default!;

        // Additional fields can be added here if needed, such as price, status, etc.
    }
}

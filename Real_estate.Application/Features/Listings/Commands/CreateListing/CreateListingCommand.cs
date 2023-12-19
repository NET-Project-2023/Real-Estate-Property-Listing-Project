﻿using MediatR;
using static Real_estate.Domain.Enums.Enums;

namespace Real_estate.Application.Features.Listings.Commands.CreateListing
{
    public class CreateListingCommand : IRequest<CreateListingCommandResponse>
    {
        public string Title { get; set; } = default!;
        public string Username { get; set; } // User who is creating the listing
        public string PropertyName { get; set; } // Property associated with the listing
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public Status PropertyStatus { get; set; }


    }
}

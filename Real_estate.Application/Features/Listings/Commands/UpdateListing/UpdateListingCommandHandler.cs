﻿using MediatR;
using Real_estate.Application.Persistence;
using Real_estate.Domain.Entities;

namespace Real_estate.Application.Features.Listings.Commands.UpdateListing
{
    public class UpdateListingCommandHandler : IRequestHandler<UpdateListingCommand, UpdateListingCommandResponse>
    {
        private readonly IAsyncRepository<Listing> listingRepository;

        public UpdateListingCommandHandler(IAsyncRepository<Listing> listingRepository)
        {
            this.listingRepository = listingRepository;
        }

        public async Task<UpdateListingCommandResponse> Handle(UpdateListingCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateListingCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new UpdateListingCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var listingResult = await listingRepository.FindByNameAsync(request.Title);
            if (!listingResult.IsSuccess)
            {
                return new UpdateListingCommandResponse
                {
                    Success = false,
                    Message = "Listing not found."
                };
            }

            var listing = listingResult.Value;
            if (request.Title != null)
            {
                listing.UpdateTitle(request.Title);
            }
            if (request.Description != null)
            {
                listing.UpdateDescription(request.Description);
            }
            if (request.Price != null)
            {
                listing.UpdatePrice(request.Price.Value);
            }
            if(request.Status != null)
            {
                listing.UpdateStatus(request.Status);
            }

            var updateResult = await listingRepository.UpdateAsync(listing);

            if (!updateResult.IsSuccess)
            {
                return new UpdateListingCommandResponse
                {
                    Success = false,
                    Message = "Failed to update listing."
                };
            }

            return new UpdateListingCommandResponse
            {
                Success = true,
                Message = "Listing updated successfully."
            };
        }
    }
}

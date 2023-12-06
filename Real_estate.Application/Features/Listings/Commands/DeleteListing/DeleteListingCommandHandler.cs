using MediatR;
using Real_estate.Application.Persistence;
using Real_estate.Domain.Entities;

namespace Real_estate.Application.Features.Listings.Commands.DeleteListing
{
    public class DeleteListingCommandHandler : IRequestHandler<DeleteListingCommand, DeleteListingCommandResponse>
    {
        private readonly IAsyncRepository<Listing> listingRepository;

        public DeleteListingCommandHandler(IAsyncRepository<Listing> listingRepository)
        {
            this.listingRepository = listingRepository;
        }

        public async Task<DeleteListingCommandResponse> Handle(DeleteListingCommand request, CancellationToken cancellationToken)
        {
            var deleteResult = await listingRepository.DeleteAsync(request.ListingId);

            if (!deleteResult.IsSuccess)
            {
                return new DeleteListingCommandResponse
                {
                    Success = false,
                    Message = deleteResult.Error
                };
            }

            return new DeleteListingCommandResponse
            {
                Success = true,
                Message = "Listing deleted successfully."
            };
        }
    }
}

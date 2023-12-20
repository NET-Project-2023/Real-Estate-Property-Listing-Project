using MediatR;
using Real_estate.Application.Persistence;
using Real_estate.Domain.Entities;

namespace Real_estate.Application.Features.Listings.Commands.CreateListing
{
    public class CreateListingCommandHandler : IRequestHandler<CreateListingCommand, CreateListingCommandResponse>
    {
        private readonly IListingRepository listingRepository;
        private readonly IUserManager userRepository;
        private readonly IPropertyRepository propertyRepository;

        public CreateListingCommandHandler(
            IListingRepository listingRepository,
            IUserManager userRepository,
            IPropertyRepository propertyRepository)
        {
            this.listingRepository = listingRepository;
            this.userRepository = userRepository;
            this.propertyRepository = propertyRepository;
        }

        public async Task<CreateListingCommandResponse> Handle(CreateListingCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateListingCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new CreateListingCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var user = await userRepository.FindByUsernameAsync(request.Username);
            var property = await propertyRepository.FindByNameAsync(request.PropertyName);

            var listingResult = Listing.Create(request.Title, request.Price, request.Username, request.PropertyName, request.Description, request.PropertyStatus);

            if (!listingResult.IsSuccess)
            {
                return new CreateListingCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { listingResult.Error }
                };
            }

            await listingRepository.AddAsync(listingResult.Value);

            return new CreateListingCommandResponse
            {
                Success = true,
                Listing = new CreateListingDto
                {
                    ListingId = listingResult.Value.ListingId,
                    Title = listingResult.Value.Title,
                    Username = listingResult.Value.Username,
                    PropertyName = listingResult.Value.PropertyName,
                    Description = listingResult.Value.Description,
                    Price = listingResult.Value.Price,
                    PropertyStatus = listingResult.Value.PropertyStatus
                }
            };
        }
    }
}

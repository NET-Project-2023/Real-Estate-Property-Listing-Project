using MediatR;
using Real_estate.Application.Persistence;

namespace Real_estate.Application.Features.Listings.Queries.GetById
{
    public class GetByIdPropertyHandler : IRequestHandler<GetByIdPropertyQuery, PropertyDto>
    {
        private readonly IPropertyRepository repository;

        public GetByIdPropertyHandler(IPropertyRepository repository)
        {
            this.repository = repository;
        }

        public async Task<PropertyDto> Handle(GetByIdPropertyQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.FindByIdAsync(request.Id);
            if (result.IsSuccess)
            {
               return new PropertyDto { 
                    PropertyId = result.Value.PropertyId,
                    Title = result.Value.Title,
                    Description = result.Value.Description,
                    Address = result.Value.Address,
                    Size = result.Value.Size,
                    Price = result.Value.Price,
                    NumberOfBathrooms = result.Value.NumberOfBathrooms,
                    NumberOfBedrooms = result .Value.NumberOfBedrooms,
                    Images = result.Value.Images,
                    UserId = result.Value.UserId,
                };
            }
            return new PropertyDto();
        }
    }
}

using MediatR;
using Real_estate.Application.Features.Properties.Commands;
using Real_estate.Application.Persistence;

namespace Real_estate.Application.Features.Properties.Queries.GetById
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
                    City = result.Value.City,
                    StreetAddress = result.Value.StreetAddress,
                    Size = result.Value.Size,
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

using MediatR;
using Real_estate.Application.Persistence;

namespace Real_estate.Application.Features.Properties.Queries.GetByOwnerUsername
{
    public class GetByOwnerUsernamePropertyHandler : IRequestHandler<GetByOwnerUsernamePropertyQuery, List<PropertyDto>>
    {
        private readonly IPropertyRepository propertyRepository;

        public GetByOwnerUsernamePropertyHandler(IPropertyRepository propertyRepository)
        {
            this.propertyRepository = propertyRepository;
        }

        public async Task<List<PropertyDto>> Handle(GetByOwnerUsernamePropertyQuery request, CancellationToken cancellationToken)
        {
            var result = await propertyRepository.FindByOwnerUsernameAsync(request.Username);
            if (result.IsSuccess)
            {
                return result.Value.Select(property => new PropertyDto
                {
                    PropertyId = property.PropertyId,
                    Title = property.Title,
                    Description = property.Description,
                    Address = property.Address,
                    Size = property.Size,
                    Price = property.Price,
                    NumberOfBathrooms = property.NumberOfBathrooms,
                    NumberOfBedrooms = property.NumberOfBedrooms,
/*                    Images = property.Images,
*/                    UserId = property.UserId
                }).ToList();
            }
            return new List<PropertyDto>();
        }
    }
}

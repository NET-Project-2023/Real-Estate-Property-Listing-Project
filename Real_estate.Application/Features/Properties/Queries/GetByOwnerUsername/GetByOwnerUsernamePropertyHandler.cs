using MediatR;
using Real_estate.Application.Features.Properties.Commands;
using Real_estate.Application.Features.Properties.Queries.GetAll;
using Real_estate.Application.Features.Properties.Queries.GetByName;
using Real_estate.Application.Persistence;

namespace Real_estate.Application.Features.Properties.Queries.GetByOwnerUsername
{
    public class GetByOwnerUsernamePropertyHandler : IRequestHandler<GetByOwnerUsernamePropertyQuery, GetByOwnerUsernamePropertyResponse>
    {
        private readonly IPropertyRepository propertyRepository;

        public GetByOwnerUsernamePropertyHandler(IPropertyRepository propertyRepository)
        {
            this.propertyRepository = propertyRepository;
        }

        public async Task<GetByOwnerUsernamePropertyResponse> Handle(GetByOwnerUsernamePropertyQuery request, CancellationToken cancellationToken)
        {
            GetByOwnerUsernamePropertyResponse response = new();
            var result = await propertyRepository.FindByOwnerUsernameAsync(request.Username);
            if (result.IsSuccess)
            {
                response.Properties = result.Value.Select(property => new PropertyDto
                {
                    PropertyId = property.PropertyId,
                    Title = property.Title,
                    Description = property.Description,
                    City = property.City,
                    StreetAddress = property.StreetAddress,
                    Size = property.Size,
                    NumberOfBathrooms = property.NumberOfBathrooms,
                    NumberOfBedrooms = property.NumberOfBedrooms,
                    Images = property.Images,
                    UserId = property.UserId
                }).ToList();
            }
            return response;
        }
    }
}

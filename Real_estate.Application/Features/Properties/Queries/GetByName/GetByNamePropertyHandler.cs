using MediatR;
using Real_estate.Application.Features.Properties.Commands;
using Real_estate.Application.Persistence;

namespace Real_estate.Application.Features.Properties.Queries.GetByName
{
    public class GetByNamePropertyHandler : IRequestHandler<GetByNamePropertyQuery, PropertyDto>
    {
        private readonly IPropertyRepository repository;

        public GetByNamePropertyHandler(IPropertyRepository repository)
        {
            this.repository = repository;
        }

        public async Task<PropertyDto> Handle(GetByNamePropertyQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.FindByNameAsync(request.Title);
            if (result.IsSuccess)
            {
                return new PropertyDto
                {
                    PropertyId = result.Value.PropertyId,
                    Title = result.Value.Title,
                    Description = result.Value.Description,
                    City = result.Value.City,
                    StreetAddress = result.Value.StreetAddress,
                    Size = result.Value.Size,
                    NumberOfBathrooms = result.Value.NumberOfBathrooms,
                    NumberOfBedrooms = result.Value.NumberOfBedrooms,
                    Images = result.Value.Images,
                    UserId = result.Value.UserId
                };
            }
            return new PropertyDto();
        }
    }
}

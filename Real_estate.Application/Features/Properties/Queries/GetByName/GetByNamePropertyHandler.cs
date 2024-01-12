using MediatR;
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
            if (result.IsSuccess && result.Value != null)
            {
                return new PropertyDto
                {
                    PropertyId = result.Value.PropertyId,
                    Title = result.Value.Title,
                    Description = result.Value.Description,
                    Address = result.Value.Address,
                    Size = result.Value.Size,
                    Price = result.Value.Price,
                    NumberOfBathrooms = result.Value.NumberOfBathrooms,
                    NumberOfBedrooms = result.Value.NumberOfBedrooms,
/*                    Images = result.Value.Images,
*/                    UserId = result.Value.UserId
                };
            }
            return null;
        }
    }
}

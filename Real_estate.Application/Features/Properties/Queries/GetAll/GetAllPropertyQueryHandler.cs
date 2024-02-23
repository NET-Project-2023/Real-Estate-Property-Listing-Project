using MediatR;
using Real_estate.Application.Features.Properties.Commands;
using Real_estate.Application.Persistence;

namespace Real_estate.Application.Features.Properties.Queries.GetAll
{

    public class GetAllPropertyQueryHandler : IRequestHandler<GetAllPropertyQuery, GetAllPropertyResponse>
    {
        private readonly IPropertyRepository repository;

        public GetAllPropertyQueryHandler(IPropertyRepository repository)
        {
            this.repository = repository;
        }

        public async Task<GetAllPropertyResponse> Handle(GetAllPropertyQuery request, CancellationToken cancellationToken)
        {
            GetAllPropertyResponse response = new();
            var result = await repository.GetAllAsync();
            if (result.IsSuccess)
            {
                response.Properties = result.Value.Select(c => new PropertyDto
                {
                   PropertyId = c.PropertyId,
                   Title= c.Title,
                   Description= c.Description,
                   City = c.City,
                   StreetAddress = c.StreetAddress,
                   Size= c.Size,
                   NumberOfBathrooms= c.NumberOfBathrooms,
                   NumberOfBedrooms= c.NumberOfBedrooms,
                   Images = c.Images,
                   UserId = c.UserId
                }).ToList();
            }
            return response;
        }
    }
}

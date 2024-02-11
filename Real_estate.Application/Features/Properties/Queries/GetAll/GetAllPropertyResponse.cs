using Real_estate.Application.Features.Properties.Commands;

namespace Real_estate.Application.Features.Properties.Queries.GetAll
{
    public class GetAllPropertyResponse
    {
        public List<PropertyDto> Properties { get; set; }
    }
}

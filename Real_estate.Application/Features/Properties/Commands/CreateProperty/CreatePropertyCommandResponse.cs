using Real_estate.Application.Responses;

namespace Real_estate.Application.Features.Properties.Commands.CreateProperty
{
    public class CreatePropertyCommandResponse : BaseResponse
    {
        public CreatePropertyCommandResponse() : base()
        {
        }
        public CreatePropertyDto Property { get; set; }
    }
}
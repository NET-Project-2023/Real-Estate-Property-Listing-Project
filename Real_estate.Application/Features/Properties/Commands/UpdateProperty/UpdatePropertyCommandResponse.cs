using Real_estate.Application.Responses;

namespace Real_estate.Application.Features.Properties.Commands.UpdateProperty
{
    public class UpdatePropertyCommandResponse : BaseResponse
    {
        public UpdatePropertyCommandResponse() : base()
        {
        }
        public string Message { get; set; }
    }
}

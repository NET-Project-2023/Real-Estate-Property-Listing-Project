using MediatR;
using Microsoft.AspNetCore.Http;

namespace Real_estate.Application.Features.Properties.Commands.UpdateProperty
{
    public class UpdatePropertyCommand : IRequest<UpdatePropertyCommandResponse>
    {
        public string? Title { get; set; } // Optional update
        public string? Description { get; set; } // Optional update
        public string? Address { get; set; } // Optional update
        public int? Size { get; set; } // Optional update
        public int? Price { get; set; } // Optional update
        public int? NumberOfBedrooms { get; set; } // Optional update
        public int? NumberOfBathrooms { get; set; } // Optional update
        public List<byte[]>? Images { get; set; } // Optional update
        public List<IFormFile> ImagesFiles { get; set; } = new List<IFormFile>();

    }
}

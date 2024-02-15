using MediatR;
using Microsoft.AspNetCore.Http;

namespace Real_estate.Application.Features.Properties.Commands.CreateProperty
{
    public class CreatePropertyCommand : IRequest<CreatePropertyCommandResponse>
    {
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public string City { get; set; }
        public string StreetAddress { get; set; }
        public int Size { get; set; }
        public int NumberOfBedrooms { get; set; } = default!;
        public int NumberOfBathrooms { get; set; } = default!;
        public string UserId { get; set; }
        public List<byte[]> Images { get; set; } = new List<byte[]>(); // Changed from List<IFormFile>
        public List<IFormFile> ImagesFiles { get; set; } = new List<IFormFile>();


    }
}
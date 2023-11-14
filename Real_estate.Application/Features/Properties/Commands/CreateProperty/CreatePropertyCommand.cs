using MediatR;
using static Real_estate.Domain.Enums.Enums;

namespace Real_estate.Application.Features.Properties.Commands.CreateProperty
{
    public class CreatePropertyCommand : IRequest<CreatePropertyCommandResponse>
    {
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public string Address { get; set; }
        public int Size { get; set; }
        public int Price { get; set; } = default!;
        public int NumberOfBedrooms { get; set; } = default!;
        public int NumberOfBathrooms { get; set; } = default!;
        public List<string>? ImagesUrls { get; set; } = new List<string>();
        public Status PropertyStatus { get; set; }
        public Guid OwnerId { get; set; }

    }
}
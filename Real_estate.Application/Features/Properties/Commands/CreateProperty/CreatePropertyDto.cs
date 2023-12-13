using static Real_estate.Domain.Enums.Enums;

namespace Real_estate.Application.Features.Listings.Commands.CreateProperty
{
    public class CreatePropertyDto
    {
        public Guid PropertyId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string Address { get; set; }
        public int Size { get; set; }
        public int Price { get; set; } = default!;
        public int NumberOfBedrooms { get; set; } = default!;
        public int NumberOfBathrooms { get; set; } = default!;
        public List<byte[]> Images { get; set; } = default!;
        public Status PropertyStatus { get; set; }
        public string OwnerUniqueName { get; set; }

    }
}
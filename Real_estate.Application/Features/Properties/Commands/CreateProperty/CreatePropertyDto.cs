namespace Real_estate.Application.Features.Properties.Commands.CreateProperty
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
        public string UserId { get; set; }

    }
}
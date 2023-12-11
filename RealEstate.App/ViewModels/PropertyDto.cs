using static RealEstate.App.Enums;

namespace RealEstate.App.ViewModels
{
    public class PropertyDto
    {
        public Guid PropertyId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string Address { get; set; }
        public int Size { get; set; }
        public int Price { get; set; }
        public int NumberOfBedrooms { get; set; }
        public int NumberOfBathrooms { get; set; }
        public List<byte[]> Images { get; set; }
        public Status PropertyStatus { get; set; }
        public Guid OwnerId { get; set; }
    }
}

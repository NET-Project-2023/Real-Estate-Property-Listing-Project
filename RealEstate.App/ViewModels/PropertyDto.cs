
namespace RealEstate.App.ViewModels
{
    public class PropertyDto
    {
        public string title { get; set; }
        public string? description { get; set; }
        public string address { get; set; }
        public int size { get; set; }
        public int price { get; set; }
        public int numberOfBedrooms { get; set; }
        public int numberOfBathrooms { get; set; }
        public List<byte[]> images { get; set; }
    }
}

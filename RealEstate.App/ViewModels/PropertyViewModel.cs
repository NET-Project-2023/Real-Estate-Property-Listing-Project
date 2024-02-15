
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;

namespace RealEstate.App.ViewModels
{
    public class PropertyViewModel
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public string City { get; set; }
        public string StreetAddress { get; set; }
        public int Size { get; set; }
        public int NumberOfBedrooms { get; set; }
        public int NumberOfBathrooms { get; set; }      
        public string UserId { get; set; }
        public List<byte[]> Images { get; set; }
        public List<IBrowserFile> ImagesFiles { get; set; } = new List<IBrowserFile>();


    }
}


using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;

namespace RealEstate.App.ViewModels
{
    public class PropertyDto
    {
        public string? Title { get; set; } 
        public string? Description { get; set; } 
        public string? Address { get; set; } 
        public int? Size { get; set; } // Optional update
        public int? Price { get; set; } // Optional update
        public int? NumberOfBedrooms { get; set; } // Optional update
        public int? NumberOfBathrooms { get; set; } // Optional update
        public List<byte[]> Images { get; set; } // Optional update
        public List<IBrowserFile> ImagesFiles { get; set; } = new List<IBrowserFile>();
        public string UserId { get; set; } 

    }
}

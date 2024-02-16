
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;

namespace RealEstate.App.ViewModels
{
    public class PropertyDto
    {
        public string? Title { get; set; } 
        public string? Description { get; set; }
        public string? City { get; set; }
        public string? StreetAddress { get; set; }
        public int? Size { get; set; } // Optional update
        public int? NumberOfBedrooms { get; set; } // Optional update
        public int? NumberOfBathrooms { get; set; } // Optional update
        public List<byte[]>? Images { get; set; } // Optional update
        public List<IBrowserFile>? ImagesFiles { get; set; } = new List<IBrowserFile>();
        public string UserId { get; set; } 

    }
}

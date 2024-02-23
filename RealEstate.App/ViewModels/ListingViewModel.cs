using System.ComponentModel.DataAnnotations;
using static RealEstate.App.Enums;

namespace RealEstate.App.ViewModels
{
    public class ListingViewModel
    {
        public Guid ListingId { get; set; }

        [Required(ErrorMessage = "Listing title is required")]
        [StringLength(100, ErrorMessage = "Max 100 characters are allowed for Title")]
        public string? Title { get; set; }
        public decimal Price { get; set; }
        public string UserName { get; set; }
        public Guid PropertyId { get; set; }
        public Status PropertyStatus { get; set; } 
        public DateTime LastModifiedAt { get; set; }
    }
}

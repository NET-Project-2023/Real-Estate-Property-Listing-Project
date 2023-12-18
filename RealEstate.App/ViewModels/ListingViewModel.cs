using System.ComponentModel.DataAnnotations;
using static RealEstate.App.Enums;

namespace RealEstate.App.ViewModels
{
    public class ListingViewModel
    {
        public Guid ListingId { get; set; }
        [Required(ErrorMessage = "Listing title is required")]
        [StringLength(50, ErrorMessage = "Max 50 characters are allowed for Title")]
        public string? Title { get; set; }
        public decimal Price { get; set; }
        public string UserName { get; set; }
        public string PropertyName { get; set; }
        public string Description { get; set; }
        public Status PropertyStatus { get; set; }
    }
}

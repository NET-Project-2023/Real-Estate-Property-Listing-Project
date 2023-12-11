using System.ComponentModel.DataAnnotations;

namespace RealEstate.App.Contracts
{
    public class ListingViewModel
    {
        public Guid ListingId { get; set; }
        [Required(ErrorMessage = "Listing title is required")]
        [StringLength(50, ErrorMessage = "Max 50 characters are allowed for Title")]
        public string Title { get; set; } = string.Empty;
        public Guid UserId { get; set; } // modificat in Guid
        public Guid PropertyId { get; set; } // modificat in Guid
        public string Description { get; set; }
    }
}

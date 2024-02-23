using Real_estate.Domain.Common;
using static Real_estate.Domain.Enums.Enums;
namespace Real_estate.Domain.Entities
{
    public class Listing : AuditableEntity
    {
        private Listing()
        {
        }

        public Listing(string? title, decimal price, string userName, Guid propertyId, Status propertyStatus)
        {
            ListingId = Guid.NewGuid();
            Title = title;
            Price = price;
            Username = userName;
            PropertyId = propertyId;
            PropertyStatus = propertyStatus;
        }

        public Guid ListingId { get; private set; }
        public string? Title { get; private set; }
        public Guid PropertyId{ get; private set; }
        public decimal Price { get; private set; }
        public string Username { get; private set; } // the owner unique username
        public Status PropertyStatus { get; private set; }


        public static Result<Listing> Create(string title, decimal price, string username, Guid propertyId, Status propertyStatus)
        {
            if (string.IsNullOrWhiteSpace(username) || propertyId == Guid.Empty)
            {
                return Result<Listing>.Failure("User and Property are required.");
            }

            if (string.IsNullOrWhiteSpace(title))
            {
                return Result<Listing>.Failure("Title is required.");
            }


            if (price <= 0)
            {
                return Result<Listing>.Failure("Price can't be smaller than 0.");
            }

            // de validat PropertyStatus

            return Result<Listing>.Success(new Listing(title, price, username, propertyId, propertyStatus));
        }
        public void UpdateTitle(string newTitle)
        {
            if (string.IsNullOrWhiteSpace(newTitle))
            {
                throw new ArgumentException("Title cannot be empty.", nameof(newTitle));
            }

            // Additional business rules can be enforced here

            Title = newTitle;
        }


        public void UpdateStatus(Status propertyStatus)
        {
            if (!propertyStatus.Equals(Status.ForRent) && !propertyStatus.Equals(Status.ForSale) && !propertyStatus.Equals(Status.SoldOrRented)) 
            {
                throw new ArgumentException("Invalid status for this listing.");
            }
            PropertyStatus = propertyStatus;


        }
        public void UpdatePrice(decimal price)
        {
            if (price <= 0)
            {
                throw new ArgumentException("Price cannot be smaller than 0.", nameof(price));
            }   
            Price = price;
        }

    }
}

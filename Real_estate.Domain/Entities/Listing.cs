using Real_estate.Domain.Common;
using static Real_estate.Domain.Enums.Enums;
namespace Real_estate.Domain.Entities
{
    public class Listing : AuditableEntity
    {
        private Listing()
        {
            // EF Core needs this constructor
        }

        public Listing(string? title, decimal price, string userName, string propertyName, string description, Status propertyStatus)
        {
            ListingId = Guid.NewGuid();
            Title = title;
            Price = price;
            Username = userName;
            PropertyName = propertyName;
            Description = description;
            PropertyStatus = propertyStatus;
        }

        public Guid ListingId { get; private set; }
        public string? Title { get; private set; }
        public decimal Price { get; private set; }
        public string Username { get; private set; }
        public string PropertyName { get; private set; }
        public string Description { get; private set; }
        public Status PropertyStatus { get; private set; }


        public static Result<Listing> Create(string title, decimal price, string username, string propertyName, string description, Status propertyStatus)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(propertyName))
            {
                return Result<Listing>.Failure("User and Property are required.");
            }

            if (string.IsNullOrWhiteSpace(title))
            {
                return Result<Listing>.Failure("Title is required.");
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                return Result<Listing>.Failure("Description is required.");
            }

            if (price <= 0)
            {
                return Result<Listing>.Failure("Price can't be smaller than 0.");
            }

            // de validat PropertyStatus

            return Result<Listing>.Success(new Listing(title, price, username, propertyName, description, propertyStatus));
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

        // Method to update the description of the listing
        public void UpdateDescription(string newDescription)
        {
            if (string.IsNullOrWhiteSpace(newDescription))
            {
                throw new ArgumentException("Description cannot be empty.", nameof(newDescription));
            }

            // Additional business rules can be enforced here

            Description = newDescription;
        }

        public void UpdateStatus(Status propertyStatus)
        {
            if (!propertyStatus.Equals(Status.ForRent) && !propertyStatus.Equals(Status.ForSale) && !propertyStatus.Equals(Status.SoldOrRented)) 
            {
                throw new ArgumentException("Invalid status for this listing.");
            }
        }

    }
}

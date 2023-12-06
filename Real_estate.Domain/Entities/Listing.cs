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
        private Listing(string title, User user, Property property, string description) : this()
        {
            ListingId = Guid.NewGuid();
            Title = title;
            User = user;
            Property = property;
            Description = description;
        }

        public Guid ListingId { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public User User { get; private set; }
        public Property Property { get; private set; }
        public string Description { get; private set; }

        public static Result<Listing> Create(string title, User user, Property property, string description)
        {
            if (user == null || property == null)
            {
                return Result<Listing>.Failure("User and Property are required.");
            }

            Role userRole = user.UserRole;

            if (string.IsNullOrWhiteSpace(title))
            {
                return Result<Listing>.Failure("Title is required.");
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                return Result<Listing>.Failure("Description is required.");
            }

            if (userRole != Role.Owner)
            {
                return Result<Listing>.Failure("Listing creator must be an Owner");
            }
            return Result<Listing>.Success(new Listing(title, user, property, description));
        }
        public void UpdateTitle(string newTitle)
        {
            if (string.IsNullOrWhiteSpace(newTitle))
            {
                throw new ArgumentException("Title cannot be empty.", nameof(newTitle));
            }

            Title = newTitle;
        }

        public void UpdateDescription(string newDescription)
        {
            // Assuming that Description can be null or empty
            Description = newDescription;
        }

        public void UpdateUser(User newUser)
        {
            if (newUser == null)
            {
                throw new ArgumentNullException(nameof(newUser), "User cannot be null.");
            }

            User = newUser;
        }

        public void UpdateProperty(Property newProperty)
        {
            if (newProperty == null)
            {
                throw new ArgumentNullException(nameof(newProperty), "Property cannot be null.");
            }

            Property = newProperty;
        }

    }
}

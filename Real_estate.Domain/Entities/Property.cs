using Real_estate.Domain.Common;
namespace Real_estate.Domain.Entities

{
    public class Property : AuditableEntity
    {
        private Property(string title, string address, int size, int price, string userId, int numberOfBedrooms)
        {
            PropertyId = Guid.NewGuid();
            Title = title;
            Address = address;
            Size = size;
            Price = price;
            UserId = userId;
            NumberOfBedrooms = numberOfBedrooms;
            Images = new List<byte[]>(); // Update this line
        }

        public Guid PropertyId { get; private set; }
        public string Title { get; private set; }
        public string? Description { get; private set; }
        public string Address { get; private set; }
        public int Size { get; private set; }
        public int Price { get; private set; }
        public int NumberOfBedrooms { get; private set; }
        public int NumberOfBathrooms { get; private set; }
        public List<byte[]> Images { get; private set; }
        public string UserId { get; private set; }

        public static Result<Property> Create(string title, string address, int size, int price, string ownerUniqueName, int numberOfBedrooms)
        {
            // Validation logic
            if (string.IsNullOrEmpty(title))
                return Result<Property>.Failure("Title cannot be null or empty.");

            if (string.IsNullOrEmpty(address))
                return Result<Property>.Failure("Address cannot be null or empty.");

            if (size <= 0)
                return Result<Property>.Failure("Size must be greater than 0.");

            if (price <= 0)
                return Result<Property>.Failure("Price must be greater than 0.");

            if (string.IsNullOrEmpty(ownerUniqueName))
                return Result<Property>.Failure("Owner's unique name cannot be null or empty.");

            if (numberOfBedrooms <= 0)
                return Result<Property>.Failure("Number of bedrooms must be greater than 0.");

            // Create the property
            return Result<Property>.Success(new Property(title, address, size, price, ownerUniqueName, numberOfBedrooms));
        }


        public void AttachDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                Description = description;
            }
        }

        public void AttachImageUrls(List<byte[]> images)
        {
            if (images == null || !images.Any())
            {
                throw new ArgumentException("Images list cannot be null or empty.", nameof(images));
            }

            Images.AddRange(images);
        }

        public void AttachNumberOfBathrooms(int numberOfBathrooms)
        {
            if (NumberOfBathrooms == 0)
            {
                NumberOfBathrooms = numberOfBathrooms;
            }
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
            if (string.IsNullOrWhiteSpace(newDescription))
            {
                throw new ArgumentException("Description cannot be empty.", nameof(newDescription));
            }
            Description = newDescription;
        }

        public void UpdateAddress(string newAddress)
        {
            if (string.IsNullOrWhiteSpace(newAddress))
            {
                throw new ArgumentException("Address cannot be empty.", nameof(newAddress));
            }
            Address = newAddress;
        }

        public void UpdateSize(int newSize)
        {
            if (newSize <= 0)
            {
                throw new ArgumentException("Size must be greater than zero.", nameof(newSize));
            }
            Size = newSize;
        }

        public void UpdatePrice(int newPrice)
        {
            if (newPrice < 0) // Assuming price cannot be negative.
            {
                throw new ArgumentException("Price cannot be negative.", nameof(newPrice));
            }
            Price = newPrice;
        }

        public void UpdateNumberOfBedrooms(int newNumberOfBedrooms)
        {
            if (newNumberOfBedrooms < 0) // Assuming the number of bedrooms cannot be negative.
            {
                throw new ArgumentException("Number of bedrooms cannot be negative.", nameof(newNumberOfBedrooms));
            }
            NumberOfBedrooms = newNumberOfBedrooms;
        }

        public void UpdateNumberOfBathrooms(int newNumberOfBathrooms)
        {
            if (newNumberOfBathrooms < 0) // Assuming the number of bathrooms cannot be negative.
            {
                throw new ArgumentException("Number of bathrooms cannot be negative.", nameof(newNumberOfBathrooms));
            }
            NumberOfBathrooms = newNumberOfBathrooms;
        }
        public static Result<Property> CreateWithDescription(string title, string address, int size, int price, string userId, int numberOfBedrooms, string description)
        {
            // Validation logic
            // ...

            // Create the property
            var property = new Property(title, address, size, price, userId, numberOfBedrooms);
            property.Description = description;

            return Result<Property>.Success(property);
        }


    }
}

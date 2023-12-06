using Real_estate.Domain.Common;
using static Real_estate.Domain.Enums.Enums;
namespace Real_estate.Domain.Entities

{
    public class Property : AuditableEntity
    {
        private Property(string title, string address, int size, int price, Status propertyStatus, Guid ownerId, int numberOfBedrooms)
        {
            PropertyId = Guid.NewGuid();
            Title = title;
            Address = address;
            Size = size;
            Price = price;
            PropertyStatus = propertyStatus;
            OwnerId = ownerId;
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

        public Status PropertyStatus { get; private set; }
        public Guid OwnerId { get; private set; }

        public static Result<Property> Create(string title, string address, int size, int price, Status propertyStatus, Guid ownerId, int numberOfBedrooms)
        {

            // Create the property
            return Result<Property>.Success(new Property(title, address, size, price, propertyStatus, ownerId, numberOfBedrooms));
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
            // Assuming that Description can be null or empty
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
            // You may add validation logic for the size if needed
            Size = newSize;
        }

        public void UpdatePrice(int newPrice)
        {
            // You may add validation logic for the price if needed
            Price = newPrice;
        }

        public void UpdateNumberOfBedrooms(int newNumberOfBedrooms)
        {
            // You may add validation logic for the number of bedrooms if needed
            NumberOfBedrooms = newNumberOfBedrooms;
        }

        public void UpdateNumberOfBathrooms(int newNumberOfBathrooms)
        {
            // You may add validation logic for the number of bathrooms if needed
            NumberOfBathrooms = newNumberOfBathrooms;
        }

        public void UpdatePropertyStatus(Status newPropertyStatus)
        {
            // You may add validation logic for the property status if needed
            PropertyStatus = newPropertyStatus;
        }
    }
}

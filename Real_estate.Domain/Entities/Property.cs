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
            ImagesUrls = new List<string>(); // Add this line
        }
        public Guid PropertyId { get; private set; } //
        public string Title { get; private set; }//
        public string? Description { get; private set; }
        public string Address { get; private set; } //
        public int Size { get; private set; } //
        public int Price { get; private set; } //
        public int NumberOfBedrooms { get; private set; } //
        public int NumberOfBathrooms { get; private set; }
        public List<string> ImagesUrls { get; private set; } //

        public Status PropertyStatus { get; private set; } //
        public Guid OwnerId { get; private set; } //

        public static Result<Property> Create( string title, string address, int size, int price, Status propertyStatus, Guid ownerId, int numberOfBedrooms)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return Result<Property>.Failure("Property Name is required.");
            }
            if (string.IsNullOrWhiteSpace(address))
            {
                return Result<Property>.Failure("Address is required.");
            }
            if (size <= 0)
            {
                return Result<Property>.Failure("Size must be greater than 0");
            }
            if (price <= 0)
            {
                return Result<Property>.Failure("Price must be greater than zero.");
            }
            if (propertyStatus != Status.ForSale && propertyStatus != Status.ForRent && propertyStatus != Status.SoldOrRented)
            {
                return Result<Property>.Failure("Must enter a valid status :  ForSale / ForRent / SoldOrRented ");
            }
            if (ownerId == default)
            {
                return Result<Property>.Failure("Owner Id should not be Guid.Empty(Default).");
            }
            if (numberOfBedrooms < 0)
            {
                return Result<Property>.Failure("Must enter a valid number of Bedrooms");

            }

            return Result<Property>.Success(new Property( title, address, size, price, propertyStatus, ownerId, numberOfBedrooms));
        }
        public string ImagesUrlsSerialized
        {
            get => string.Join(";", ImagesUrls);
            private set => ImagesUrls = value.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public void AttachDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                Description = description;
            }
        }
        public void AttachImageUrls(List<string> imagesUrl)
        {
            if (imagesUrl == null || !imagesUrl.Any())
            {
                throw new ArgumentException("Images list cannot be null or empty.", nameof(imagesUrl));
            }

            ImagesUrls.AddRange(imagesUrl);
        }
        public void AttachNumberOfBathrooms(int numberOfBathrooms)
        {
            if (NumberOfBathrooms == 0)
            {
                NumberOfBathrooms = numberOfBathrooms;
            }
        }

    }
}

using Real_estate.Domain.Common;
namespace Real_estate.Domain.Entities

{
    public class Property : AuditableEntity
    {
          
        private Property(string title, string city, string streetAddress, int size, string userId, int numberOfBedrooms, List<byte[]> images)
        {
            PropertyId = Guid.NewGuid();
            Title = title;
            City = city;
            StreetAddress = streetAddress;
            Size = size;
            UserId = userId;
            NumberOfBedrooms = numberOfBedrooms;
            Images = images ?? new List<byte[]>();
        }
     

        public Guid PropertyId { get; private set; }
        public string Title { get; private set; }
        public string? Description { get; private set; }
        public string City { get; private set; }
        public string StreetAddress { get; private set; }
        public int Size { get; private set; }
        public int NumberOfBedrooms { get; private set; }
        public int NumberOfBathrooms { get; private set; }
        public List<byte[]> Images { get; private set; }
        public string UserId { get; private set; }

        public static Result<Property> Create(string title, string city, string streetAddress, int size, string ownerUniqueName, int numberOfBedrooms, List<byte[]> images)
        {
            var property = new Property(title, city, streetAddress, size, ownerUniqueName, numberOfBedrooms, images);
            return Result<Property>.Success(property);
        }


        public void AttachDescription(string description)
        {
             Description = description;
        }

        public void AttachImageUrls(List<byte[]> images)
        {
            if (images == null || !images.Any())
            {
                throw new ArgumentException("Images list cannot be null or empty.", nameof(images));
            }

            foreach (var image in images)
            {
                Images.Add(image);
            }

        }

        public void UpdateImages(List<byte[]> images)
        {
            Images = images;
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
            // additional validations
            Description = newDescription;
        }

        public void UpdateStreetAddress(string newStreetAddress)
        {
            if (string.IsNullOrWhiteSpace(newStreetAddress))
            {
                throw new ArgumentException("Address cannot be empty.", nameof(newStreetAddress));
            }
            StreetAddress = newStreetAddress;
        }

        public void UpdateCity(string newCity)
        {
            if (string.IsNullOrEmpty(newCity))
            {
                throw new ArgumentException("City cannot be null.", nameof(newCity));
            }
        }

        public void UpdateSize(int newSize)
        {
            if (newSize <= 0)
            {
                throw new ArgumentException("Size must be greater than zero.", nameof(newSize));
            }
            Size = newSize;
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

    }
}

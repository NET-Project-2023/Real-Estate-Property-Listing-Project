
namespace PropertyListing.Server.Domain.Entities
{
    public class Property
    {
        public Guid PropertyId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Address { get; private set; }
        public int Size { get; private set; }
        public int Price { get; private set; }
        public Status PropertyStatus { get; private set; }
        public Guid OwnerId { get; private set; }
        public DateTime Created_at { get; private set; }
        public DateTime Updated_at { get; private set; }

        public Property(Guid propertyId, string title, string description, string address, int size, int price, Status propertyStatus, Guid ownerId, DateTime created_at, DateTime updated_at)
        {
            PropertyId = propertyId;
            Title = title;
            Description = description;
            Address = address;
            Size = size;
            Price = price;
            PropertyStatus = propertyStatus;
            OwnerId = ownerId;
            Created_at = created_at;
            Updated_at = updated_at;
        }
    }
    public enum Status
    {
        ForSale,
        ForRent,
        SoldOrRented
    }
}

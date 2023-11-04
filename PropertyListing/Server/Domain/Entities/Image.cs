namespace PropertyListing.Server.Domain.Entities
{
    public class Image
    {
        public Guid ImageId { get; private set; }
        public Guid PropertyId { get; private set; }
        public string Path { get; private set; }

        public Image(Guid imageId, Guid propertyId, string path)
        {
            ImageId = imageId;
            PropertyId = propertyId;
            Path = path;
        }
    }
}

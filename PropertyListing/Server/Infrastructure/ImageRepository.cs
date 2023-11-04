using Microsoft.EntityFrameworkCore;
using PropertyListing.Server.Domain.Entities;
using PropertyListing.Server.Domain.Interfaces;

namespace PropertyListing.Server.Infrastructure
{
    public class ImageRepository : GenericRepository<Image>, IImageRepository
    {
        public ImageRepository(DbContext context) : base(context)
        {
        }
    }
}

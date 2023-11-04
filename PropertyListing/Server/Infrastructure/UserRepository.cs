using Microsoft.EntityFrameworkCore;
using PropertyListing.Server.Domain.Entities;
using PropertyListing.Server.Domain.Interfaces;

namespace PropertyListing.Server.Infrastructure
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }
    }
}

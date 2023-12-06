using Microsoft.EntityFrameworkCore;
using Real_estate.Application.Persistence;
using Real_estate.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(RealEstateContext context) : base(context)
        {

        }

        public async Task<bool> UserExistsAsync(Guid userId)
        {
            return await ExistsAsync(u => u.UserId == userId);
        }
    }
}

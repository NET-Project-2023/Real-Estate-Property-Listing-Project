using Real_estate.Application.Persistence;
using Real_estate.Domain.Entities;

namespace Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(RealEstateContext context) : base(context)
        {

        }
    }
}

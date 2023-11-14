using Real_estate.Application.Contracts;
using Real_estate.Domain.Entities;

namespace Real_estate.Application.Persistence
{
    public interface IUserRepository : IAsyncRepository<User>
    {
    }
}

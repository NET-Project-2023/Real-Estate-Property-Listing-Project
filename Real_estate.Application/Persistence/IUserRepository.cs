using System;
using System.Threading.Tasks;
using Real_estate.Domain.Entities;

namespace Real_estate.Application.Persistence
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        Task<bool> UserExistsAsync(Guid userId);
    }
}

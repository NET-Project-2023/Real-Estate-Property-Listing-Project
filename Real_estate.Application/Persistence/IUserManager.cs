using Real_estate.Application.Features.Listings;
using Real_estate.Domain.Common;

namespace Real_estate.Application.Persistence
{
    public interface IUserManager
    {
        Task<Result<UserDto>> FindByIdAsync(string userId);
        Task<Result<UserDto>> FindByEmailAsync(string email);
        Task<Result<UserDto>> FindByUsernameAsync(string username);
        Task<Result<List<UserDto>>> GetAllAsync();
        Task<Result<UserDto>> DeleteAsync(Guid userId);
        Task<Result<UserDto>> UpdateAsync(UserDto user);
        //Task<bool> UserExistsAsync(string userId);

        //Task<Result<UserDto>> UpdateRoleAsync(UserDto user, string role);
    }
}

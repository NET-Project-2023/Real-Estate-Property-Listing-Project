using RealEstate.App.Services;
using RealEstate.App.ViewModels;
using RealEstate.App.Services.Responses;

namespace RealEstate.App.Contracts
{
    public interface IUserService
    {
        Task<ApiResponse<UserViewModel>> UpdateUser(UserViewModel updateUserModel);
        Task<UserViewModel> GetUser(string username);
        Task<List<UserViewModel>> GetAllUsers();
        Task<ApiResponse<bool>> DeleteUser(string username);
    }
}

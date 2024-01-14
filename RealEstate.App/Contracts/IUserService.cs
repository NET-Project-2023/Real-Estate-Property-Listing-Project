using RealEstate.App.Services;
using RealEstate.App.ViewModels;
using RealEstate.App.Services.Responses;

namespace RealEstate.App.Contracts
{
    public interface IUserService
    {
        Task<ApiResponse<UpdateUserViewModel>> UpdateUser(UpdateUserViewModel updateUserModel);

        Task<UpdateUserViewModel> GetUser(string username);
    }
}

using RealEstate.App.Services.Responses;
using RealEstate.App.ViewModels;

namespace RealEstate.App.Contracts
{
    public interface IPropertyDataService
    {
        Task<List<PropertyViewModel>> GetPropertiesAsync();
        Task<ApiResponse<PropertyViewModel>> CreatePropertyAsync(PropertyViewModel propertyViewModel);
        Task<PropertyViewModel> GetPropertyByIdAsync(Guid propertyId);
    }
}

using RealEstate.App.Services.Responses;
using RealEstate.App.ViewModels;

namespace RealEstate.App.Contracts
{
    public interface IPropertyDataService
    {
        Task<List<PropertyViewModel>> GetPropertiesAsync();
        Task<ApiResponse<PropertyDto>> CreatePropertyAsync(PropertyViewModel propertyViewModel);
        Task<List<PropertyViewModel>> GetPropertyByIdAsync(Guid propertyId);
        Task<PropertyDto> GetPropertyByNameAsync(string propertyName);
        Task<List<PropertyViewModel>> GetPropertiesByCurrentUserAsync();
        Task<ApiResponse<PropertyDto>> UpdatePropertyAsync(PropertyDto propertyDto);

    }
}

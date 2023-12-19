using RealEstate.App.Services.Responses;
using RealEstate.App.ViewModels;

namespace RealEstate.App.Contracts
{
    public interface IPropertyDataService
    {
        Task<List<PropertyViewModel>> GetPropertiesAsync();
        Task<ApiResponse<PropertyDto>> CreatePropertyAsync(PropertyViewModel propertyViewModel);
        Task<PropertyViewModel> GetPropertyByIdAsync(Guid propertyId);
        Task<PropertyDto> GetPropertyByNameAsync(string propertyName);

        Task<ApiResponse<PropertyDto>> UpdatePropertyAsync(PropertyDto propertyDto);

    }
}

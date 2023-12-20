using RealEstate.App.ViewModels;

namespace RealEstate.App.Services.Responses
{
    public class PropertiesResponse
    {
        public List<PropertyViewModel> Properties { get; set; }
        public PropertyViewModel Property { get; set; }
    }
}

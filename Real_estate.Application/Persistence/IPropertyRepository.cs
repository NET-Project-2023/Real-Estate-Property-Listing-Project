using Real_estate.Application.Features.Properties.Queries;
using Real_estate.Domain.Common;
using Real_estate.Domain.Entities;


namespace Real_estate.Application.Persistence
{
    public interface IPropertyRepository : IAsyncRepository<Property>
    {
        Task<Result<List<Property>>> FindByOwnerUsernameAsync(string ownerUsername);
    }
}
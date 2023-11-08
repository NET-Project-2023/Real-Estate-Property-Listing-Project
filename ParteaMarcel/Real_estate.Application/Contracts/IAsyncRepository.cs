/*using Real_estate.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_estate.Application.Contracts
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<Result<T>> UpdateAsync(T entity);
        Task<Result<T>> FindByIdAsync(Guid id);
        Task<Result<T>> AddAsync(T entity);
        Task<Result<T>> DeleteAsync(Guid id);
        Task<Result<IReadOnlyList<T>>> GetPagedReponseAsync(int page, int size);
    }
}
*/
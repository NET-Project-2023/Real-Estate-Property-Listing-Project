﻿using Real_estate.Domain.Common;
using System.Linq.Expressions;

namespace Real_estate.Application.Persistence
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<Result<T>> UpdateAsync(T entity);
        Task<Result<T>> FindByIdAsync(Guid id);
        Task<Result<T>> AddAsync(T entity);
        Task<Result<IReadOnlyList<T>>> GetAllAsync();

        Task<Result<T>> DeleteAsync(Guid id);
        Task<Result<IReadOnlyList<T>>> GetPagedReponseAsync(int page, int size);
        Task<Result<T>> FindByNameAsync(string title);
        Task<Result<T>> FindByConditionAsync(Expression<Func<T, bool>> predicate);
    }
}

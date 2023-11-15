using Microsoft.EntityFrameworkCore;
using Real_estate.Application.Contracts;
using Real_estate.Domain.Common;
namespace Infrastructure.Repositories
{
    public class BaseRepository<T> : IAsyncRepository<T> where T : class
    {
        private readonly RealEstateContext context;

        public BaseRepository(RealEstateContext context)
        {
            this.context = context;
        }

        public async Task<Result<T>> AddAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
            return Result<T>.Success(entity);
        }


        public async Task<Result<T>> DeleteAsync(Guid id)
        {
            var result = await FindByIdAsync(id);
            if (!result.IsSuccess)
            {
                return Result<T>.Failure($"Entity with id {id} not found ");

            }
            context.Set<T>().Remove(result.Value);
            await context.SaveChangesAsync();
            return Result<T>.Success(result.Value);

        }

        public async Task<Result<T>> FindByIdAsync(Guid id)
        {
            var result = await context.Set<T>().FindAsync(id);
            if (result == null)
            {
                return Result<T>.Failure($"Entity with id {id} not found");
            }
            return Result<T>.Success(result);
        }

        public async Task<Result<IReadOnlyList<T>>> GetPagedReponseAsync(int page, int size)
        {
            var result = await context.Set<T>().Skip(page).Take(size).AsNoTracking().ToListAsync();

            return Result<IReadOnlyList<T>>.Success(result);


        }


        public async Task<Result<T>> UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Result<T>.Success(entity);
        }
        public async Task<Result<IReadOnlyList<T>>> GetAllAsync()
        {
            var result = await context.Set<T>().AsNoTracking().ToListAsync();
            return Result<IReadOnlyList<T>>.Success(result);

        }
        public async Task<Result<T>> FindByNameAsync(string title)
        {
            var propertyInfo = typeof(T).GetProperty("Title");
            if (propertyInfo == null || propertyInfo.PropertyType != typeof(string))
            {
                return Result<T>.Failure("Entity does not have a 'Title' property of type string.");
            }

            var entity = await context.Set<T>()
                .FirstOrDefaultAsync(e => EF.Functions.Like(EF.Property<string>(e, "Title"), title));

            if (entity == null)
            {
                return Result<T>.Failure($"Entity with title '{title}' not found");
            }

            return Result<T>.Success(entity);
        }


    }
}

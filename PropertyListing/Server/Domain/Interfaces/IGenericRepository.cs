namespace PropertyListing.Server.Domain.Interfaces
{
    //public interface IGenericRepository<TEntity, TKey> where TEntity : class
    //{
    //    Task<TEntity> GetByIdAsync(TKey id);
    //    Task<List<TEntity>> GetAllAsync();
    //    Task AddAsync(TEntity entity);
    //    Task UpdateAsync(TEntity entity);
    //    Task DeleteAsync(TKey id);
    //}
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void Remove(TEntity entity);
        void Update(TEntity entity);
        TEntity FindById(object id);
        List<TEntity> FindAll();
    }
}

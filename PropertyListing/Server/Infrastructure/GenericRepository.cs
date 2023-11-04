using Microsoft.EntityFrameworkCore;
using PropertyListing.Server.Domain.Interfaces;

namespace PropertyListing.Server.Infrastructure
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        private DbSet<TEntity> _entities;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            _entities.Add(entity);
        }

        public List<TEntity> FindAll()
        {
            return _entities.ToList();
        }

        public TEntity FindById(object id)
        {
            return _entities.Find(id)!;
        }

        public void Remove(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _entities.Update(entity);
        }
    }
}

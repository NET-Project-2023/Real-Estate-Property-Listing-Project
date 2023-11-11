using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EasyImplementation
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly MyDbContext _context;
        private DbSet<TEntity> _entities;

        public Repository(MyDbContext context)//implementeaza toate metodele de la interfata IRpeository(asta inseamna ca implementeaza o interfata)

        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            _entities.Add(entity);
            _context.SaveChanges();
        }

        public void Remove(TEntity entity)
        {
            _entities.Remove(entity);
            _context.SaveChanges();
        }

        public TEntity FindById(object id)
        {
            return _entities.Find(id)!;
        }
    }
}

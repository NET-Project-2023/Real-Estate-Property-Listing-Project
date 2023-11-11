using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EasyImplementation
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);//add si remove(comenzii) modifica starea sistemului
        void Remove(TEntity entity);
        TEntity FindById(object id);
    }
}

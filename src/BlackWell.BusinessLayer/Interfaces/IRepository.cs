using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackWell.BusinessLayer.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity item);

        void Delete(TEntity item);

        void Update(TEntity item);

        IEnumerable<TEntity> GetAll();

        IQueryable<TEntity> GetQueryable();
    }
}

using BlackWell.BusinessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackWell.BusinessLayer.Repositories
{
    public class BaseRepository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class
        where TContext : DbContext
    {
        protected readonly TContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(TContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<TEntity>();
        }
        public void Add(TEntity item)
        {
            _dbContext.Set<TEntity>().Add(item);
        }

        public void Delete(TEntity item)
        {
            _dbSet.Remove(item);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().AsEnumerable();
        }
        public IQueryable<TEntity> GetQueryable()
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }

        public void Update(TEntity item)
        {
            _dbContext.Set<TEntity>().Attach(item);
            _dbContext.Entry(item).State = EntityState.Modified;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackWell.BusinessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlackWell.BusinessLayer.Context
{
    public class SQLUnitOfWork : ISQLUnitOfWork
    {
        private readonly IServiceScope serviceScope;
        private bool disposedValue;
        public DbContext dbContext { get; private set; }

        public SQLUnitOfWork(
            DbContext context,
            IServiceProvider serviceProvider)
        {
            serviceScope = serviceProvider.CreateScope();
            dbContext = context;
        }

        public IRepository<TEntity> UseRepository<TEntity>() where TEntity : class
        {
            return serviceScope.ServiceProvider.GetService<IRepository<TEntity>>();
        }

        public int Commit()
        {
            return dbContext.SaveChanges();
        }

        public Task<int> CommitAsync()
        {
            return dbContext.SaveChangesAsync();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    serviceScope.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}

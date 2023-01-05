using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackWell.BusinessLayer.Interfaces
{
    public interface ISQLUnitOfWork : IDisposable
    {
        IRepository<TEntity> UseRepository<TEntity>() where TEntity : class;
        DbContext dbContext { get; }

        int Commit();
        Task<int> CommitAsync();
    }
}

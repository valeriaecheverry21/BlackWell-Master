using BlackWell.BusinessLayer.Context;
using BlackWell.BusinessLayer.Entities;
using BlackWell.BusinessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackWell.BusinessLayer.Repositories
{
    public class UserRepository : BaseRepository<User, DataContext>, IUserRepository, IRepository<User>
    {
        public UserRepository(DataContext context) : base(context)
        {
        }

        public User GetByName(string name)
        {
            return _dbSet.AsEnumerable().FirstOrDefault(c => c.Name == name);
        }

        public User GetById(int id)
        {
            return _dbSet.AsEnumerable().FirstOrDefault(c => c.Id == id);
        }
    }
}

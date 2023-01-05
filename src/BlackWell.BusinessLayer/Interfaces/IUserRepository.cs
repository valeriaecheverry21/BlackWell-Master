using BlackWell.BusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackWell.BusinessLayer.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByName(string name);
        User GetById(int id);
    }
}

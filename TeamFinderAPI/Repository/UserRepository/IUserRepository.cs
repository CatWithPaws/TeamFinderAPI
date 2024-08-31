using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamFinderAPI.DB.Models;

namespace TeamFinderAPI.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public void Save();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamFinderAPI.Data;
using TeamFinderAPI.DB.Models;

namespace TeamFinderAPI.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(TeamFindAPIContext context) : base(context){}

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
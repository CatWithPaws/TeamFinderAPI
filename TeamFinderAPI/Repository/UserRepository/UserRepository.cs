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

        public User FindByLogin(string login) 
        {
            return _context.Set<User>().FirstOrDefault(e => e.Login.Equals(login));
        }

        public bool UserWithLoginExists(string login){
            return _context.Set<User>().Any(e => e.Login == login);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
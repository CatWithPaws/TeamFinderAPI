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
        public User FindByLogin(string login);
        public bool UserWithLoginExists(string login);
        public bool UserWithGoogleTokenExists(string token);
        public User FindByGoogleId(string googleId);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamFinderAPI.Data;

namespace TeamFinderAPI.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(TeamFindAPIContext context) : base(context){}
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamFinderAPI.DB.Models;

namespace TeamFinderAPI.Repository
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        public IEnumerable<Post> FindPostsBySearch(string query);
        public void Save();
    }
}
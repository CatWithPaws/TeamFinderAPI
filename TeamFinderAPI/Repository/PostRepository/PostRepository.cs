using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamFinderAPI.Data;
using TeamFinderAPI.DB.Models;

namespace TeamFinderAPI.Repository.PostReposity
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public PostRepository(TeamFindAPIContext context) : base(context)
        {
        }
    }
}
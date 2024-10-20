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

        public IEnumerable<Post> FindPostsBySearch(string query)
        {
            if(query.Contains('#')){
                return GetPostsByTags(query);
            }
            return GetPostsByWords(query);
        }

        public IEnumerable<Post> GetPostsByTags(string query){
            var tags = query.Split('#').ToList();
            return _context.Set<Post>()
                     .Where(e => e.Tags.Split('#',StringSplitOptions.None)
                            .ToList()
                            .Any(t => tags.Contains(t)))
                     .ToList();
        }

        public IEnumerable<Post> GetPostsByWords(string query){
            query = query.ToLower();
            string[] words = query.Split(' ');
            return _context.Set<Post>()
                     .Where(e => 
                            e.Title.Split(' ',StringSplitOptions.None)
                                .ToList()
                                .Any(t => words.Contains(t.ToLower())) || 
                            e.Text.Split(' ',StringSplitOptions.None)
                                .ToList()
                                .Any(t => words.Contains(t.ToLower())))
                     .ToList();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
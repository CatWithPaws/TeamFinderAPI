using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamFinderAPI.Controllers.PostBody
{
    //Need for AddPost method
    public class CreatePostBody
    {
        public string name { get; set; }  = String.Empty;
        
        public int createdUserId { get; set; }
        public string game { get; set; } = String.Empty;
        public string type { get; set; } = String.Empty;
        public string text { get; set; } = String.Empty;
        public string tags { get; set; } = String.Empty;
        public DateTime createdDate { get; set;} = DateTime.MinValue;
        public DateTime modifiedDate { get; set;} = DateTime.MinValue;
    }
}
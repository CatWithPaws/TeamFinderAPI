using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Util;

namespace TeamFinderAPI.Controllers.PostBody
{
    //Need for AddPost method
    public class CreatePostBody
    {
        public string title { get; set; }  = String.Empty;
        
        public string game { get; set; } = String.Empty;
        public string type { get; set; } = String.Empty;
        public string comment { get; set; } = String.Empty;
        public string[] tags { get; set; } = new string[0];
        public Socials socials { get; set; } = null;
    }

    public class Socials{
        public string? telegram {get; set; } = String.Empty;
        
        public string? discord {get; set; } = String.Empty;

    }
}
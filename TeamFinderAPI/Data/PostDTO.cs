using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamFinderAPI.DB.Models;

namespace TeamFinderAPI.Data
{
    public class PostDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }  = String.Empty;
        
        public UserDTO CreatedByUser{ get; set; }
        public string Game { get; set; } = String.Empty;
        public string Text { get; set; } = String.Empty;
        public string[] Tags { get; set; } = new string[0];
        public DateTime CreatedDate { get; set;} = DateTime.MinValue;

        public Socials Socials { get; set; } = null;

    }
    public class Socials{
        public string Discord { get; set;} = String.Empty; 
        public string Telegram { get; set; } = String.Empty;
    }
    
}
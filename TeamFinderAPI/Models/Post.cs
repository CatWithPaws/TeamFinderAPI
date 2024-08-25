using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TeamFinderAPI.Data;

namespace TeamFinderAPI.Models
{
    public class Post
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }  = String.Empty;
        
        public User CreatedBy { get; set; }
        public string Game { get; set; } = String.Empty;
        public string Text { get; set; } = String.Empty;
        public string Tags { get; set; } = String.Empty;
        public DateTime CreatedDate { get; set;} = DateTime.MinValue;
        public DateTime ModifiedDate { get; set;} = DateTime.MinValue;

    }
}
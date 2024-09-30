using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TeamFinderAPI.Data;

namespace TeamFinderAPI.DB.Models
{
    public class Post
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = String.Empty;
        [Required]
        public PostType Type { get; set; } = PostType.None;
        [Required]
        public User CreatedBy { get; set; }
        public int CreatedById { get; set; }
        public string Game { get; set; } = String.Empty;
        [Required]
        public string Text { get; set; } = String.Empty;
        public string Tags { get; set; } = String.Empty;
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.MinValue;
        public DateTime ModifiedDate { get; set; } = DateTime.MinValue;
        public Post()
        {

        }
        public Post(string name, string type, int userCreated, string game, string text, string tags)
        {
            Name = name;
            CreatedById = userCreated;
            Game = game;
            Text = text;
            Tags = tags;
            Type = ToPostType(type);
            CreatedDate = DateTime.UtcNow;
        }

        private static PostType ToPostType(string stringType)
        {
            switch (stringType)
            {
                case "lookingForPlayers":   return PostType.LookingForPlayers;
                case "lookingForGroup":     return PostType.LookingForGroup;
                default:                    return PostType.None;
            }
        }
    }
}
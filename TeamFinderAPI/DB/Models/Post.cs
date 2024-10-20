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
        public string Title { get; set; } = String.Empty;
        [Required]
        public PostType Type { get; set; } = PostType.None;
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [Required]
        public int UserId { get; set; }
        public string Game { get; set; } = String.Empty;
        [Required]
        public string Text { get; set; } = String.Empty;
        public string Tags { get; set; } = String.Empty;
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.MinValue;
        public DateTime ModifiedDate { get; set; } = DateTime.MinValue;

        public string? TelegramLink { get; set; } = String.Empty;
        public string? Discord {get; set;} = String.Empty;
        public Post()
        {

        }
        public Post(string name, string type, int userCreated, string game, string text, string tags)
        {
            Title = name;
            UserId = userCreated;
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
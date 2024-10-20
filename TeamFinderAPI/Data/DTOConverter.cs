using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using TeamFinderAPI.DB.Models;

namespace TeamFinderAPI.Data
{
    public static class DTOConverter
    {
        
        public static UserDTO ToDTO(this User user){
            return new UserDTO{
                ID = user.Id,
                Name = user.Login,
                Email = user.Email,
                TelegramLink = user.TelegramLink,
                DiscordUsername = user.DiscordUsername,
            };
        }
        public static PostDTO ToDTO(this Post post){
            if(post.User == null) throw new ArgumentNullException("USER IS NULL");
            return new PostDTO{
                Id = post.Id,
                Name = post.Title,
                Game = post.Game,
                Text = post.Text,
                Tags = post.Tags.Split('#'),
                CreatedByUser = post.User.ToDTO(),
                CreatedDate = post.CreatedDate,
                Socials = new Socials{
                    Telegram = post.TelegramLink,
                    Discord = post.Discord
                }

            };
        }
    }
}

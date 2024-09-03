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
                ID = user.ID,
                Name = user.Login,
                Email = user.Email,
            };
        }
        public static PostDTO ToDTO(this Post post){
            return new PostDTO{
                Id = post.Id,
                Name = post.Name,
                Game = post.Game,
                Text = post.Text,
                Tags = post.Tags,
                CreatedByUser = post.CreatedBy.ToDTO(),
                CreatedDate = post.CreatedDate,

            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamFinderAPI.Controllers.PostBody
{
    public class UpdatePlayerBody
    {
        public string DisplayName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        
        public string TelegramLink { get; set; } = string.Empty;
        public string DiscordUsername { get; set; } = string.Empty;
    }
}
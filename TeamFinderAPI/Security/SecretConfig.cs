using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamFinderAPI.Security
{
    public class SecretConfig
    {
        public static string GoogleClientId { get; set; } = string.Empty;
        public static string GoogleSecret { get; set; } = string.Empty;
        public static string DiscordClientSecret { get; set; } = string.Empty;
        public static string DiscordClientId { get; set; } = string.Empty;

        public static string RefreshTokenSecret { get; set; } = string.Empty;
    }
}
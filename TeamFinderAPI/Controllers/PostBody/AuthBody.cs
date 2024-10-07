using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamFinderAPI.Controllers.PostBody
{
    public class AuthBody
    {
        public string Type { get; set; } = String.Empty;
        public string Name { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        
    }
}
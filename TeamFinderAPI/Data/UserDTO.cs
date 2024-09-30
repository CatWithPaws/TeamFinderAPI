using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamFinderAPI.DB.Models;

namespace TeamFinderAPI.Data
{
    public class UserDTO
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;       
        public string Email { get; set; } = string.Empty;

    }

    
}
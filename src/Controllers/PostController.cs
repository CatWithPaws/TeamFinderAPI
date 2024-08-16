using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TeamFindApi{
    
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        [HttpGet]
        public Post GetAllPost(){
            
            return null;
        }
    }
}
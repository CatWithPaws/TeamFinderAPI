using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeamFinderAPI.Data;
using TeamFinderAPI.Repository;

namespace server.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository){
        _userRepository = userRepository;
    }

        [HttpGet]
        public IEnumerable<User> GetUsers(){
             return _userRepository.GetAll();
        }
    }
}
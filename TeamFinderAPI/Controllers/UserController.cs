using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeamFinderAPI.Data;
using TeamFinderAPI.Models;
using TeamFinderAPI.Repository;

namespace server.Controllers
{


    [ApiController]
    [Route("api/v0/users")]
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

        [HttpGet("{id}")]
        public User GetUser(int id){
            return _userRepository.GetById(id);
        }

        [HttpPost("add")]
        public void AddUser([FromBody] User user){
            _userRepository.Add(user);
        }
        

        [HttpDelete]
        public void RemoveUser([FromBody] User user){
            _userRepository.Remove(user);
        }
        
    }
}
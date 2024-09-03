using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeamFinderAPI.Controllers.PostBody;
using TeamFinderAPI.Data;
using TeamFinderAPI.DB.Models;
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
        public IActionResult AddUser([FromBody] RegistrationBody user){
            
            if(_userRepository.UserWithLoginExists(user.Name)){
                return BadRequest("User already exists");
            }
            
            User newUser = new User{
                Login = user.Name,
                Email = user.Email,
                Password = user.Password
            } ;

            _userRepository.Add(newUser);
            _userRepository.Save();
            return Ok();
        }
        

        [HttpDelete]
        public void RemoveUser([FromBody] User user){
            _userRepository.Remove(user);
            _userRepository.Save();
        }
        
        [HttpGet("save")]
        public void Save(){
            _userRepository.Save();
        }
    }
}
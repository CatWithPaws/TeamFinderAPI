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
        public void AddUser([FromBody] CreateUserBody user){
            User newUser = new User{
                Name = user.Name,
                Email = user.Email,
                Password = user.Password
            } ;

            _userRepository.Add(newUser);
            
        }
        

        [HttpDelete]
        public void RemoveUser([FromBody] User user){
            _userRepository.Remove(user);
        }
        
        [HttpGet("save")]
        public void Save(){
            _userRepository.Save();
        }
    }
}
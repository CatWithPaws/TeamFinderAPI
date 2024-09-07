using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamFinderAPI.Controllers.PostBody;
using TeamFinderAPI.Repository;
using TeamFinderAPI.Security;

namespace TeamFinderAPI.Controllers.ReturnObjects
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository){
            _userRepository = userRepository;
        }

         [AllowAnonymous]
         [HttpPost]
        public IActionResult Auth([FromBody] AuthBody authBody)
        {
        if (CheckUser(authBody.Username, authBody.Password))
        {
            string token = TokenManager.GenerateToken(authBody.Username);
            return Ok(token);
        }
        return Unauthorized();
        }


        private bool CheckUser(string username, string password){
            
            var User = _userRepository.FindByLogin(username);
            Console.WriteLine("User is null? " + User == null);
            if(User == null) return false;

            System.Security.Cryptography.HashAlgorithm hashAlgo = new System.Security.Cryptography.SHA256Managed();

            byte[] passBytes = Encoding.UTF8.GetBytes(password);
            byte[] hash = hashAlgo.ComputeHash(passBytes);
            if(!User.Password.SequenceEqual(hash)){
                return false;
            }

            return true;
        }
    }
}
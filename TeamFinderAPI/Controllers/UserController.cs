using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Timers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TeamFinderAPI.Controllers.PostBody;
using TeamFinderAPI.DB.Models;
using TeamFinderAPI.Helper;
using TeamFinderAPI.JwtAuthentication;
using TeamFinderAPI.Repository;

namespace server.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {

        private readonly IUserRepository _userRepository;
        
       

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            
        }

        [HttpGet("{id}")]
        public User GetUser(int id)
        {
            return _userRepository.GetById(id);
        }

        [HttpGet("logout")]
        public async Task<IResult> LogOut()
        {
            HttpContext ctx = HttpContext;
            var jwt = await ctx.GetTokenAsync("access_token");
            var decipher = new JwtSecurityTokenHandler();
            JwtSecurityToken obj = decipher.ReadJwtToken(jwt);

            return Results.Ok(obj);
        }


        [HttpDelete]
        public void RemoveUser([FromBody] User user)
        {
            _userRepository.Remove(user);
            _userRepository.Save();
        }

        private bool CheckUser(string username, string password)
        {

            var User = _userRepository.FindByLogin(username);
            if (User == null) return false;

            System.Security.Cryptography.HashAlgorithm hashAlgo = new System.Security.Cryptography.SHA256Managed();

            byte[] passBytes = Encoding.UTF8.GetBytes(password);
            byte[] hash = hashAlgo.ComputeHash(passBytes);
            if (!User.Password.SequenceEqual(hash))
            {
                return false;
            }

            return true;
        }

    }
}
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Timers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TeamFinderAPI.Controllers.PostBody;
using TeamFinderAPI.Data;
using TeamFinderAPI.DB.Models;
using TeamFinderAPI.Helper;
using TeamFinderAPI.JwtAuthentication;
using TeamFinderAPI.Repository;
using TeamFinderAPI.Service;

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
        public IResult GetUser(int id)
        {
            User user = _userRepository.GetById(id);
            if(user == null){
                return Results.NotFound();
            }
            return Results.Ok(user.ToDTO());
        }

        [HttpGet("getmyself")]
        public async Task<IResult> GetUserByToken(){
            var token = await Utils.DecipherToken(HttpContext);

            var claim = token.Claims.FirstOrDefault(c => c.Type == "name");
            if(claim == null){ return Results.NotFound(); }
            
            string username = claim.Value;
            User user = _userRepository.FindBy(i => i.Login == username);
            
            if(user == null){
                return Results.BadRequest("Something went wrong");
            }

            return Results.Ok(user.ToDTO());
        }

        [HttpGet("logout")]
        public async Task<IResult> LogOut()
        {
            var token = await Utils.DecipherToken(HttpContext);
            var claim = token.Claims.FirstOrDefault(c => c.Type == "name");
            if(claim == null){ return Results.NotFound(); }
            string username = claim.Value;
            User user = _userRepository.FindBy(i => i.Login == username);
            if(user == null){
                return Results.BadRequest("Something went wrong");
            }

            user.RefreshToken = "";
            _userRepository.Save();
            return Results.Ok();
        }
        
        [HttpPut("update")]
        public async Task<IResult> UpdateUser([FromBody] UpdatePlayerBody userToUpdate){

            var token = await Utils.DecipherToken(HttpContext);

            var name = token.Claims.FirstOrDefault(c => c.Type == "name").Value;
            if(name == null) { return Results.BadRequest(); }

            var user = _userRepository.FindBy(u => u.Login == name);
            
            if(user == null){ return Results.NotFound();}

            if(user.Login != name){
                return Results.BadRequest();
            }

            user.TelegramLink = userToUpdate.TelegramLink;
            user.DiscordUsername = userToUpdate.DiscordUsername;
            user.DisplayName = userToUpdate.DisplayName;

            _userRepository.Save();

            return Results.Ok(user.ToDTO());
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
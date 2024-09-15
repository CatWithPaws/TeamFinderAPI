using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TeamFinderAPI.Controllers.PostBody;
using TeamFinderAPI.Data;
using TeamFinderAPI.DB.Models;
using TeamFinderAPI.JwtAuthentication;
using TeamFinderAPI.Repository;
using TeamFinderAPI.Security;

namespace server.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {

        private readonly IUserRepository _userRepository;
        
        private readonly List<(string,DateTime)> blackList ;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Log in as registered user
        /// </summary>
        /// <param name="body">user credentials</param>
        /// <returns>JWT token</returns>
        [AllowAnonymous]
        [HttpPost("auth")]
        public IResult Connect(
        JwtOptions jwtOptions, [FromBody]AuthBody body)
        {

            User foundUser = _userRepository.FindByLogin(body.Name);

            if(foundUser == null ){
                return Results.BadRequest("USer not found");
            }
            System.Security.Cryptography.HashAlgorithm hashAlgo = System.Security.Cryptography.SHA256.Create();

            byte[] passBytes = Encoding.UTF8.GetBytes(body.Password);
            byte[] hashedPass = hashAlgo.ComputeHash(passBytes);
            

            if(!foundUser.Password.SequenceEqual(hashedPass)){
                return Results.BadRequest("password is incorrect");
            }

            //creates the access token (jwt token)
            var tokenExpiration = TimeSpan.FromSeconds(jwtOptions.ExpirationSeconds);
            var accessToken = CreateAccessToken(
                jwtOptions,
                body.Name,
                TimeSpan.FromMinutes(5),
                new[] { "read_todo", "create_todo" });

            //returns a json response with the access token
            return Results.Ok(new
            {
                access_token = accessToken,
                expiration = (int)tokenExpiration.TotalSeconds,
                type = "bearer",
                username = body.Name
            });
        }

        
        [AllowAnonymous]
        [HttpPost("googleauth")]
        public IActionResult GoogleHook([FromRoute] string code){
            return Ok(code);
        }

        string CreateAccessToken(
        JwtOptions jwtOptions,
        string username,
        TimeSpan expiration,
        string[] permissions)
        {
            var keyBytes = Encoding.UTF8.GetBytes(jwtOptions.SigningKey);
            var symmetricKey = new SymmetricSecurityKey(keyBytes);

            var signingCredentials = new SigningCredentials(
                symmetricKey,
                SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
    {
        new Claim("sub", username),
        new Claim("name", username),
        new Claim("aud", jwtOptions.Audience)
    };

            var roleClaims = permissions.Select(x => new Claim("role", x));
            claims.AddRange(roleClaims);

            var token = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                claims: claims,
                expires: DateTime.Now.Add(expiration),
                signingCredentials: signingCredentials);

            var rawToken = new JwtSecurityTokenHandler().WriteToken(token);
            return rawToken;
        }

        [HttpGet("{id}")]
        public User GetUser(int id)
        {
            return _userRepository.GetById(id);
        }

        [HttpGet("logout")]
        public IResult LogOut(JwtOptions jwtOptions){
            return Results.Ok();
        }

        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="user">user's data for registration</param>
        [AllowAnonymous]
        [HttpPost("register")]
        public IResult AddUser(JwtOptions jwtOptions,[FromBody] RegistrationBody user)
        {

            if (_userRepository.UserWithLoginExists(user.Name))
            {
                return Results.BadRequest("User already exists");
            }
            System.Security.Cryptography.HashAlgorithm hashAlgo = System.Security.Cryptography.SHA256.Create();
            byte[] passBytes = Encoding.UTF8.GetBytes(user.Password);
            byte[] hashedPass = hashAlgo.ComputeHash(passBytes);
            User newUser = new User
            {
                Login = user.Name,
                Email = user.Email,
                Password = hashedPass
            };

            _userRepository.Add(newUser);
            _userRepository.Save();

            var accessToken = CreateAccessToken(
                jwtOptions,
                user.Name,
                TimeSpan.FromMinutes(5),
                new[] { "read_todo", "create_todo" });


            var tokenExpiration = TimeSpan.FromSeconds(jwtOptions.ExpirationSeconds);
            //returns a json response with the access token
            return Results.Ok(new
            {
                access_token = accessToken,
                expiration = (int)tokenExpiration.TotalSeconds,
                type = "bearer",
                username = user.Name
            });
        }


        [HttpDelete]
        public void RemoveUser([FromBody] User user)
        {
            
            _userRepository.Remove(user);
            _userRepository.Save();
        }

        [HttpGet("save")]
        public void Save()
        {
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
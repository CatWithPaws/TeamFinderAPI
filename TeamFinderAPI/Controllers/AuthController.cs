using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TeamFinderAPI.Controllers.PostBody;
using TeamFinderAPI.DB.Models;
using TeamFinderAPI.Helper;
using TeamFinderAPI.JwtAuthentication;
using TeamFinderAPI.Repository;
using Microsoft.AspNetCore.Cors;

namespace TeamFinderAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        private readonly List<(string, DateTime)> blackList;
        System.Timers.Timer blacklistCheckTimer;

        private const string RedirectUrl = "http://lequilesoftware.fun/api/auth/google/success";
        private const string PkceSessionKey = "codeVerifier";
        private const string GoogleScope = "profile";

        private const string GoogleClientId = "334288315445-2vjeicc4u1hfpasr2ha0uckg4hjt86v4.apps.googleusercontent.com";
        private const string GoogleClientSecret = "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";
        


        public AuthController(IUserRepository repository)
        {
            _userRepository = repository;
            blacklistCheckTimer = new System.Timers.Timer(5000);
            blacklistCheckTimer.Elapsed += (a, b) => { CheckBlackList(); };
            blacklistCheckTimer.Enabled = true;
            blacklistCheckTimer.AutoReset = true;
        }

        private void CheckBlackList()
        {
            Console.WriteLine("Checking blackList");
            var NowDate = DateTime.UtcNow;

            foreach (var item in blackList)
            {
                if (NowDate > item.Item2)
                {
                    blackList.Remove(item);
                }
            }
        }


        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="user">user's data for registration</param>
        [HttpPost("register")]
        public IResult AddUser(JwtOptions jwtOptions, [FromBody] RegistrationBody user)
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

        /// <summary>
        /// Login by username and password
        /// </summary>
        /// <param name="jwtOptions"></param>
        /// <param name="body">User credentials</param>
        /// <returns></returns>
        [HttpPost("login")]
        public IResult AuthByLogin(JwtOptions jwtOptions, AuthBody body)
        {

            User foundUser = _userRepository.FindByLogin(body.Name);
            if (foundUser == null)
            {
                return Results.BadRequest("User not found");
            }
            System.Security.Cryptography.HashAlgorithm hashAlgo = System.Security.Cryptography.SHA256.Create();

            byte[] passBytes = Encoding.UTF8.GetBytes(body.Password);
            byte[] hashedPass = hashAlgo.ComputeHash(passBytes);

            if (!foundUser.Password.SequenceEqual(hashedPass))
            {
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
        /// <summary>
        /// Log in by google
        /// </summary>
        /// <returns></returns>
        [HttpGet("google")]
        public async Task<IResult> GoogleAuth(JwtOptions jwtOptions,[FromQuery] string googleToken)
        {

            GoogleProfileInfoAnswer userProfile = await HttpClientHelper.SendGetRequest<GoogleProfileInfoAnswer>("https://www.googleapis.com/oauth2/v2/userinfo",
                                                           new Dictionary<string, string>() { },
                                                           googleToken);

            if(!_userRepository.UserWithGoogleTokenExists(userProfile.id)){
                await CreateNewGoogleUser(userProfile);
            }

            return AuthorizeGoogleUser(jwtOptions,userProfile.id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="googleToken"></param>
        /// <returns>Google Id</returns>
        private async Task CreateNewGoogleUser(GoogleProfileInfoAnswer userProfile){
            var user = new User();
            

            user.Login = userProfile.email.Replace("@gmail.com","");
            user.Email = userProfile.email;
            user.GoogleId = userProfile.id;

            _userRepository.Add(user);
            _userRepository.Save();

        }

        private IResult AuthorizeGoogleUser(JwtOptions jwtOptions,string googleId){
            User googleUser = _userRepository.FindByGoogleId(googleId);

            if(googleUser == null){ return Results.NotFound("Something went wrong");}

            var tokenExpiration = TimeSpan.FromSeconds(jwtOptions.ExpirationSeconds);
            var accessToken = CreateAccessToken(
                jwtOptions,
                googleUser.Login,
                TimeSpan.FromMinutes(5),
                new[] { "read_todo", "create_todo" });
            
            return Results.Ok(new
            {
                access_token = accessToken,
                expiration = (int)tokenExpiration.TotalSeconds,
                type = "bearer",
                username = googleUser.Login
            });
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
    }



    public class GoogleProfileInfoAnswer{
        public string id { get; set; }
        public string email { get; set; }
        public string verified_email { get; set; }
        public string name { get; set; }
        public string given_name { get; set; }
        public string family_name { get; set; }
        public string picture { get; set; }

        public GoogleProfileInfoAnswer(string id,string email, string verified_email, string name, string given_name, string family_name, string picture){
            this.id = id;
            this.email = email;
            this.verified_email = verified_email;
            this.name = name;
            this.given_name = given_name;
            this.family_name = family_name;
            this.picture = picture;
        }
    }
}
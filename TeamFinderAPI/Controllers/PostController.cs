using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TeamFinderAPI.Controllers.PostBody;
using TeamFinderAPI.Data;
using TeamFinderAPI.DB.Models;
using TeamFinderAPI.Repository;
using TeamFinderAPI.Service;

namespace TeamFinderAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/post")]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        
        public PostController(IPostRepository postRepository, IUserRepository userRepository){
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        private readonly int _postCount = 15;

        [HttpGet("list/{page}")]
        public IResult GetAll([FromRoute]int page = 1){
            var posts =  _postRepository.GetAll().Skip(_postCount * (page -1)).Take(_postCount).ToList();
            if(posts.Count == 0) return Results.BadRequest();
            PostDTO[] DTO = new PostDTO[posts.Count()];
            int currIndex = 0;
            foreach(Post post in posts){
                if(post == null) continue;
                DTO[currIndex] = post.ToDTO();
                currIndex++;
            }
            
            return Results.Ok(DTO);
        }

        [HttpGet("search")]
        public async Task<IResult> Search([FromQuery] string query){
            
            
            return Results.Ok(_postRepository.FindPostsBySearch(query));
        }

        [HttpGet]
        public async Task<IResult> GetAll(){
            return Results.Ok(_postRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IResult GetById([FromRoute]int id){
            Post post = _postRepository.GetById(id);
            return Results.Ok(new{
                Id = post.Id,
                Name = post.Title,
                Game = post.Game,
                Text = post.Text,
                Tags = post.Tags,
                AuthorUsername = post.User.Login,
                Socials = new{
                    discord = post.User.DiscordUsername,
                    telegram = post.User.TelegramLink
                }
            });
        }
        

        [HttpPost("add")]
        public async Task<IResult> AddNew([FromBody] CreatePostBody post){
            
            var token = await Utils.DecipherToken(HttpContext);
            
            var name = token.Claims.FirstOrDefault(c => c.Type == "name");
            if(name == null) { return Results.BadRequest(); }
            var user = _userRepository.FindByLogin(name.Value);
            for(int i = 1; i < post.tags.Length;i++){
                post.tags[i] = '#' + post.tags[i];
            }
            Post newPost = new Post(post.title,post.type,user.Id,post.game,post.comment,String.Concat(post.tags));
            newPost.TelegramLink = post.socials.telegram;
            newPost.Discord = post.socials.discord;
            _postRepository.Add(newPost);
            _postRepository.Save();
            return Results.Ok();
        }

        [HttpDelete("delete/{id}")]
        public void DeletePost([FromRoute] int id){
            _postRepository.Remove(_postRepository.GetById(id));
            _postRepository.Save();
        }
    }
}
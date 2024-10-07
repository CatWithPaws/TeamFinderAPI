using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamFinderAPI.Controllers.PostBody;
using TeamFinderAPI.Data;
using TeamFinderAPI.DB.Models;
using TeamFinderAPI.Repository;

namespace TeamFinderAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/post")]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;

        
        public PostController(IPostRepository postRepository){
            _postRepository = postRepository;
        }

        private readonly int _postCount = 12;

        [HttpGet("list/{page}")]
        public IEnumerable<PostDTO> GetAll([FromRoute]int page = 1){
            var posts =  _postRepository.GetAll().Skip(_postCount * (page -1)).Take(_postCount);

            PostDTO[] DTO = new PostDTO[posts.Count()];
            int currIndex = 0;
            foreach(var post in posts){
                DTO[currIndex] = post.ToDTO();
                currIndex++;
            }
            
            return DTO;
        }


        [HttpGet("{id}")]
        public PostDTO GetById([FromRoute]int id){
            return _postRepository.GetById(id).ToDTO();
        }
        

        [HttpPost("add")]
        public IActionResult AddNew([FromBody] CreatePostBody post){
           
            Post newPost = new Post(post.name,post.type,post.createdUserId,post.game,post.text,post.tags);
            _postRepository.Add(newPost);
            _postRepository.Save();
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public void DeletePost([FromRoute] int id){
            _postRepository.Remove(_postRepository.GetById(id));
            _postRepository.Save();
        }
    }
}
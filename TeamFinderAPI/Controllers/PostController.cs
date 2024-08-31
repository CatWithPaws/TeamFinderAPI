using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeamFinderAPI.Controllers.PostBody;
using TeamFinderAPI.DB.Models;
using TeamFinderAPI.Repository;

namespace TeamFinderAPI.Controllers
{
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
        public IEnumerable<Post> GetAll([FromRoute]int page = 1){
            return _postRepository.GetAll().Skip(_postCount * (page -1)).Take(_postCount);
        }

        [HttpGet("{id}")]
        public Post GetById([FromRoute]int id){
            return _postRepository.GetById(id);
        }

        [HttpPost("add")]
        public IActionResult AddNew([FromBody] CreatePostBody post){
           
            Post newPost = new Post(post.name,post.createdUserId,post.game,post.text,post.tags);
            _postRepository.Add(newPost);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public void DeletePost([FromRoute] int id){
            _postRepository.Remove(_postRepository.GetById(id));
        }
    }
}
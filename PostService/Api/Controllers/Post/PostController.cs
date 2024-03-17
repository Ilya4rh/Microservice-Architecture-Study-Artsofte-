using Api.Controllers.Post.Requests;
using Api.Controllers.Post.Responses;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Domain.Entities;

namespace Api.Controllers.Post
{
    [Route("api/posts")]
    public class PostController(IPostService postService) : ControllerBase
    {
        private readonly IPostService postService = postService;

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllPostsAsync()
        {
            var posts = await postService.GetAllPostsAsync();

            return Ok(posts);
        }

        [HttpGet("{id}")]
        [ProducesResponseType<PostInfoResponse>(200)]
        public async Task<IActionResult> GetPostAsync([FromQuery] Guid postId)
        {
            var post = await postService.GetPostAsync(postId);
            
            return Ok(new PostInfoResponse 
            { 
                Id = post.Id, 
                UserId = post.UserId, 
                Title = post.Title, 
                Description = post.Description 
            });
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreatePostResponse), 200)]
        public async Task<ActionResult> CreateNewPostAsync([FromBody] CreatePostRequest postRequest)
        {
            var newPostId = await postService.CreatePostAsync(new Domain.Entities.Post
            {
                UserId = postRequest.UserId,
                Title = postRequest.Title,
                Description = postRequest.Description
            });

            return Ok(new CreatePostResponse { Id = newPostId });
        }
    }
}

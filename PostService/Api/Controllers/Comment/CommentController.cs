using Api.Controllers.Comment.Requests;
using Api.Controllers.Comment.Responses;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Api.Controllers.Comment
{
    [Route("api/comments")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService commentService;

        [HttpGet("{id}")]
        [ProducesResponseType<CommentInfoResponse>(200)]
        public async Task<IActionResult> GetComment([FromQuery] Guid id)
        {
            var newPost = await commentService.GetCommentAsync(id);

            return Ok(new CommentInfoResponse
            {
                Id = newPost.Id,
                PostId = newPost.Id,
                UserId = newPost.Id,
                Text = newPost.Text,
            });
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateCommentResponse), 200)]
        public async Task<IActionResult> CreateNewComment([FromBody] CreateCommentRequest commentRequest)
        {
            var newCommentId = await commentService.CreateCommentAsync(new Domain.Entities.Comment
            {
                UserId = commentRequest.UserId,
                PostId= commentRequest.PostId,
                Text= commentRequest.Text,
            });

            return Ok(new CreateCommentResponse { Id = newCommentId });
        }
    }
}

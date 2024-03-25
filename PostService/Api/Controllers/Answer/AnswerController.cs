using Api.Controllers.Answer.Requests;
using Api.Controllers.Answer.Responses;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Api.Controllers.Answer
{
    [Route("api/answers")]
    public class AnswerController(IAnswerService answerService) : ControllerBase
    {
        private readonly IAnswerService answerService = answerService;

        [HttpGet("{id}")]
        [ProducesResponseType<AnswerInfoResponse>(200)]
        public async Task<IActionResult> GetAnswerAsync([FromQuery] Guid id)
        {
            var answer = await answerService.GetAnswerAsync(id);

            return Ok(new AnswerInfoResponse
            {
                Id = answer.Id,
                CommentId = answer.CommentId,
                UserId = answer.UserId,
                Text = answer.Text,
            });
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateAnswerResponse), 200)]
        public async Task<IActionResult> CreateAnswerAsync([FromBody] CreateAnswerRequest answerRequest)
        {
            var newAnswerId = await answerService.CreateAnswerAsync(new Domain.Entities.Answer 
            {
                CommentId = answerRequest.CommentId,
                UserId = answerRequest.UserId,                
                Text = answerRequest.Text
            });

            return Ok(new CreateAnswerResponse { Id = newAnswerId });
        }
    }
}

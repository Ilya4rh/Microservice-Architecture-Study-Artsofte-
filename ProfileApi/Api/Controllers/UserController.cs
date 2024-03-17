using Logic.Users.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ProfileApi.Controllers.User.Requests;
using ProfileApi.Controllers.User.Responses;
using Logic.Users.Models;
using Profile.Controllers.User.Responses;

namespace ProfileApi.Controllers
{
    [Route("api/users")]
    public class UserController(IUserLogicManager userLogicManager) : ControllerBase
    {
        private readonly IUserLogicManager userLogicManager = userLogicManager;

        [HttpGet]
        [ProducesResponseType<UsersResponse>(200)]
        public async Task<IActionResult> GetUsersAsync()
        {
            var users = await userLogicManager.GetUsersAsync();

            return Ok(new UsersResponse { Users = users });
        }

        [HttpGet("{id}")]
        [ProducesResponseType<UserInfoResponse>(200)]
        public async Task<IActionResult> GetUserInfoAsync([FromQuery] Guid id)
        {
            var user = await userLogicManager.GetUserLogicAsync(id);

            return Ok(new UserInfoResponse
            {
                Id = id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Login = user.Login,
                Email = user.Email,
                Role = user.Role
            });
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateUserResponse), 200)]
        public async Task<ActionResult> CreateUserAsync([FromBody] CreateUserRequests createUserRequests)
        {
            var userId = await userLogicManager.CreateUserAsync(new UserLogic
            {
                Login = createUserRequests.Login,
                FirstName = createUserRequests.FirstName,
                LastName = createUserRequests.LastName,
                Email = createUserRequests.Email,
                Role = createUserRequests.Role
            });

            return Ok(new CreateUserResponse { Id = userId });
        }

        [HttpDelete]
        [ProducesResponseType(typeof(DeleteUsersResponse), 200)]
        public async Task<ActionResult> DeleteUsersAsync([FromBody] DeleteUsersRequests deleteUsersRequests)
        {
            var users = await userLogicManager.GetRemainingUsersAsync(deleteUsersRequests.UsersId);

            return Ok(new DeleteUsersResponse { Users = users });
        }
    }
}
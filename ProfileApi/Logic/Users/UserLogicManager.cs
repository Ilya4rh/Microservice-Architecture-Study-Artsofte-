using Dal.Users.Interfaces;
using Dal.Users.Models;
using Logic.Users.Interfaces;
using Logic.Users.Models;

namespace Logic.Users;

public class UserLogicManager : IUserLogicManager
{
    private readonly IUserRepository userRepository;

    public async Task<IEnumerable<UserLogic>> GetUsersAsync()
    {
        var users = await userRepository.GetUsersAsync();

        return users.Select(u => new UserLogic
        {
            Login = u.Login,
            FirstName = u.FirstName,
            LastName = u.LastName,
            Email = u.Email,
            Role = u.Role
        });
    }

    public async Task<Guid> CreateUserAsync(UserLogic user)
    {
        return await userRepository.CreateUserAsync(new UserDal
        {
            Login = user.Login,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Role = user.Role,
        });
    }

    public async Task<UserLogic> GetUserLogicAsync(Guid userId)
    {
        var user = await userRepository.GetUserDalAsync(userId);

        return new UserLogic
        {
            Login = user.Login,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Role = user.Role,
        };
    }

    public async Task<IEnumerable<UserLogic>> GetRemainingUsersAsync(IEnumerable<Guid> ids)
    {
        var users = await userRepository.GetRemainingUsersAsync(ids);

        return users.Select(u => new UserLogic
        {
            Login = u.Login,
            FirstName = u.FirstName,
            LastName = u.LastName,
            Email = u.Email,
            Role = u.Role
        });
    }
}
using Dal.Users.Models;

namespace Dal.Users.Interfaces;

/// <summary>
/// Хранение пользователей
/// </summary>
public interface IUserRepository
{
    Task<IEnumerable<UserDal>> GetUsersAsync();

    Task<UserDal> GetUserDalAsync(Guid userId);

    Task<Guid> CreateUserAsync(UserDal user);

    Task<IEnumerable<UserDal>> GetRemainingUsersAsync(IEnumerable<Guid> ids);
}
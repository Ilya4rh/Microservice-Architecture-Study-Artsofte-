using Dal.Users.Interfaces;
using Dal.Users.Models;
using System.Collections.Concurrent;

namespace Dal.Users;

public class UserRepository : IUserRepository
{
    private static readonly ConcurrentDictionary<Guid, UserDal> usersData = new();

    /// <summary>
    /// Возвращает всех пользователей
    /// </summary>
    /// <returns>все имеющиеся пользователи</returns>
    public async Task<IEnumerable<UserDal>> GetUsersAsync()
    {
        return usersData.Values;
    }

    /// <summary>
    /// Создает нового пользователя
    /// </summary>
    /// <param name="user"> Новый пользователь </param>
    /// <returns> Индентефикатор нового пользователя</returns>
    /// <exception cref="Exception"> Не получилось добавить пользователя </exception>
    public async Task<Guid> CreateUserAsync(UserDal user)
    {
        if (user.Id == Guid.Empty)
            user = user with { Id = Guid.NewGuid() };
        if (usersData.TryAdd(user.Id, user))
            return user.Id;

        throw new Exception("The user has not been added");
    }

    /// <summary>
    /// Возвращает пользователя
    /// </summary>
    /// <param name="userId"> Индентефикатор пользователя, которого нужно вернуть</param>
    /// <returns> Пользователь </returns>
    /// <exception cref="Exception"> Не получилось найти пользователя </exception>
    public async Task<UserDal> GetUserDalAsync(Guid userId)
    {
        if (usersData.TryGetValue(userId, out var user))
            return user;

        throw new Exception("The user was not found");
    }

    /// <summary>
    /// Возвращает оставшихся после удаления пользователей
    /// </summary>
    /// <param name="ids"> Список индентификаторов пользователей, которых нужно удалить </param>
    /// <returns> Список оставшихся пользователей </returns>
    /// <exception cref="Exception"> Не найден пользователь для удаления </exception>
    public async Task<IEnumerable<UserDal>> GetRemainingUsersAsync(IEnumerable<Guid> ids)
    {
        foreach (var id in ids)
        {
            if (usersData.TryGetValue(id, out var user))
                usersData.Remove(id, out user);
            else
                throw new Exception("The user was not found");
        }

        return usersData.Values;
    }
}
using Logic.Users.Models;

namespace Logic.Users.Interfaces;

/// <summary>
/// Работа с пользователем
/// </summary>
public interface IUserLogicManager
{
    /// <summary>
    /// Получить всех пользователей
    /// </summary>
    /// <returns> Список пользователей </returns>
    Task<IEnumerable<UserLogic>> GetUsersAsync();

    /// <summary>
    /// Получить пользователя по индентификатору
    /// </summary>
    /// <param name="userId"> индентификатор </param>
    /// <returns> Пользователь </returns>
    Task<UserLogic> GetUserLogicAsync(Guid userId);

    /// <summary>
    /// Создание нового пользователя
    /// </summary>
    /// <param name="user"> Пользователь для создания </param>
    /// <returns> Индетификатор нового пользователя </returns>
    Task<Guid> CreateUserAsync(UserLogic user);

    /// <summary>
    /// Получить оставшихся поле удаления пользователей
    /// </summary>
    /// <param name="ids"> Индентификаторы для удаления </param>
    /// <returns> Список пользователей </returns>
    Task<IEnumerable<UserLogic>> GetRemainingUsersAsync(IEnumerable<Guid> ids);
}
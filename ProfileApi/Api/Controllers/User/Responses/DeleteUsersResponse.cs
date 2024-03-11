using Logic.Users.Models;

namespace Profile.Controllers.User.Responses;

/// <summary>
/// Для ответа на запрос удаления пользователей
/// </summary>
public record DeleteUsersResponse
{
    /// <summary>
    /// Список оставшихся после удаления пользователей
    /// </summary>
    public required IEnumerable<UserLogic> Users { get; init; }
}
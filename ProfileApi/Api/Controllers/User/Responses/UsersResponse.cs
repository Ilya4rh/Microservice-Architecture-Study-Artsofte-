using Logic.Users.Models;

namespace ProfileApi.Controllers.User.Responses;

/// <summary>
/// Для ответа на запрос о всех пользователях
/// </summary>
public record UsersResponse
{
    /// <summary>
    /// Список всех пользователей
    /// </summary>
    public required IEnumerable<UserLogic> Users { get; init; }
}
namespace ProfileApi.Controllers.User.Requests;

/// <summary>
/// Для запроса по удалению пользователей
/// </summary>
public record DeleteUsersRequests
{
    /// <summary>
    /// Список индентификаторов пользователей, которых нужно удалить
    /// </summary>
    public required IEnumerable<Guid> UsersId { get; init; }
}
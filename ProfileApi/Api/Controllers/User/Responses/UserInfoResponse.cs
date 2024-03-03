namespace ProfileApi.Controllers.User.Responses;

/// <summary>
/// Для ответа на запрос о пользователе 
/// </summary>
public record UserInfoResponse
{
    /// <summary>
    /// Индентификатор
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary>
    /// Логин
    /// </summary>
    public required string Login { get; init; }

    /// <summary>
    /// Фамилия
    /// </summary>
    public required string LastName { get; init; }

    /// <summary>
    /// Имя
    /// </summary>
    public required string FirstName { get; init; }

    /// <summary>
    /// Адрес электронной почты
    /// </summary>
    public required string Email { get; init; }

    /// <summary>
    /// Роль
    /// </summary>
    public required string Role { get; init; }
}
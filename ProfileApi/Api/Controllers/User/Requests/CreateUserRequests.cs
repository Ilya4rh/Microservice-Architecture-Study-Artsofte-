namespace ProfileApi.Controllers.User.Requests;

/// <summary>
/// Для запроса создания пользователя
/// </summary>
public record CreateUserRequests
{
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
namespace ProfileApi.Controllers.User.Responses;

/// <summary>
/// Для ответа на запрос по созданию пользователя
/// </summary>
public class CreateUserResponse
{
    /// <summary>
    /// Индентификатор пользователя
    /// </summary>
    public required Guid Id { get; init; }
}
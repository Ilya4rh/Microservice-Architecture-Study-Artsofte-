using ExampleCore.Dal.Base;

namespace Dal.Users.Models;

public record UserDal : BaseEntityDal<Guid>
{
    /// <summary>
    /// Логин
    /// </summary>
    public required string Login { get; init; }

    /// <summary>
    /// Имя
    /// </summary>
    public required string LastName { get; init; }

    /// <summary>
    /// Фамилия
    /// </summary>
    public required string FirstName { get; init; }

    /// <summary>
    /// Адрес почты
    /// </summary>
    public required string Email { get; init; }

    /// <summary>
    /// Роль
    /// </summary>
    public required string Role { get; init; }
}
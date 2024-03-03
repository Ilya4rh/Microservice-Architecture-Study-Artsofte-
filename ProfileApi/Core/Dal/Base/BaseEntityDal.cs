namespace Core.Dal.Base;

/// <summary>
/// Базовая сущность для работы с объектами БД
/// </summary>
public record BaseEntityDal
{
    /// <summary>
    /// Индентификатор
    /// </summary>
    public Guid Id { get; init; }
}
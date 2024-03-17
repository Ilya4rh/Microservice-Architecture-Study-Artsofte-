using ExampleCore.Dal.Base;

namespace Domain.Entities;

/// <summary>
/// Публикация
/// </summary>
public record class Post : BaseEntityDal<Guid>
{
    /// <summary>
    /// Индентификатор пользователя, опубликовавшего публикацию
    /// </summary>
    public required Guid UserId { get; init; }
    /// <summary>
    /// Название публикации
    /// </summary>
    public required string Title { get; init; }
    /// <summary>
    /// Описание публикации
    /// </summary>
    public required string Description { get; init; }
}
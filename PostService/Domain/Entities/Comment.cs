using Core.Dal.Base;

namespace Domain.Entities;

/// <summary>
/// Комментарий к публикации
/// </summary>
public record Comment : BaseEntityDal<Guid>
{
    /// <summary>
    /// Индентификатор публикации, к которой написан комментарий
    /// </summary>
    public required Guid PostId { get; init; }

    /// <summary>
    /// Индентификатор пользователя, который написал комментарий
    /// </summary>
    public required Guid UserId { get; init; }

    /// <summary>
    /// Текст комментария
    /// </summary>
    public required string Text { get; init; }
}

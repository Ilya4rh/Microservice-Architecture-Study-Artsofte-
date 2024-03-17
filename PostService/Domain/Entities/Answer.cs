using ExampleCore.Dal.Base;

namespace Domain.Entities;

/// <summary>
/// Ответ к комментарию
/// </summary>
public record Answer : BaseEntityDal<Guid>
{
    /// <summary>
    /// Индентификатор пользователя, который оставил ответ
    /// </summary>
    public required Guid UserId { get; init; }

    /// <summary>
    /// Индетификатор комментария, к которому принадлежит ответ
    /// </summary>
    public required Guid CommentId { get; init; }

    /// <summary>
    /// Текст ответа
    /// </summary>
    public required string Text { get; init; }
}

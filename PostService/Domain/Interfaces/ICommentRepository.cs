using Domain.Entities;

namespace Domain.Interfaces;

/// <summary>
/// Для внешнего взаимодействия с комментариями
/// </summary>
public interface ICommentRepository
{
    /// <summary>
    /// Возвращает один комментарий
    /// </summary>
    /// <param name="commentId"> Индентификатор комментария, который нужно вернуть </param>
    /// <returns> Комментарий </returns>
    Task<Comment> GetCommentAsync(Guid commentId);

    /// <summary>
    /// Метод для добавления комментария
    /// </summary>
    /// <param name="comment"> Комментарий </param>
    /// <returns> Индентификатор добавленного комментария </returns>
    Task AddCommentAsync(Comment comment);
}

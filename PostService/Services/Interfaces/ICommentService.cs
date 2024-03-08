using Domain.Entities;

namespace Services.Interfaces;

/// <summary>
/// Бизнес-логика для сущности Comment. Включает в методы для работы с комментариями.
/// </summary>
public interface ICommentService
{
    /// <summary>
    /// Возвращает один комментарий по индентификатору
    /// </summary>
    /// <param name="commentId"> Индентификатор комментария, который нужно вернуть </param>
    /// <returns> Полученный по индентификатору комментарий </returns>
    Task<Comment> GetCommentAsync(Guid commentId);

    /// <summary>
    /// Метод для создания комментария
    /// </summary>
    /// <param name="comment"> Комментарий, который нужно создать </param>
    /// <returns> Индентификатор созданного комментария </returns>
    Task<Guid> CreateCommentAsync(Comment comment);
}

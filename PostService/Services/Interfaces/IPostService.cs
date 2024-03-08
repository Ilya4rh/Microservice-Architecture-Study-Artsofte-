using Domain.Entities;

namespace Services.Interfaces;

/// <summary>
/// Бизнес-логика для сущности Post. Включает в методы для работы с публикациями.
/// </summary>
public interface IPostService
{
    /// <summary>
    /// Возвращает все публикации, которые имеются на данный момент
    /// </summary>
    /// <returns> Список публикаций </returns>
    Task<IEnumerable<Post>> GetAllPostsAsync();

    /// <summary>
    /// Возвращает одну публикацию по индентификатору
    /// </summary>
    /// <param name="postId"> Индентификатор публикации, которую нужно вернуть </param>
    /// <returns> Полученная по индентификатору публикация </returns>
    Task<Post> GetPostAsync(Guid postId);

    /// <summary>
    /// Метод для создания публикации
    /// </summary>
    /// <param name="post"> Публикация, которую нужно создать </param>
    /// <returns> Индентификатор созданной публикации </returns>
    Task<Guid> CreatePostAsync(Post post);
}

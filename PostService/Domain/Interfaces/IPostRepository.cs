using Domain.Entities;

namespace Domain.Interfaces;

/// <summary>
/// Для внешнего взаимодействия с публикациями
/// </summary>
public interface IPostRepository
{
    /// <summary>
    /// Возвращает все публикации, которые имеются на данный момент
    /// </summary>
    /// <returns> Список публикаций </returns>
    Task<IEnumerable<Post>> GetAllPostsAsync();

    /// <summary>
    /// Возвращает одну публикацю
    /// </summary>
    /// <param name="postId"> Индентификатор публикации, которую нужно вернуть </param>
    /// <returns> Публикация </returns>
    Task<Post> GetPostAsync(Guid postId);

    /// <summary>
    /// Метод для добавления публикации
    /// </summary>
    /// <param name="post"> Публикация </param>
    /// <returns> Индентификатор добавленной публикации </returns>
    Task AddPostAsync(Post post);
}

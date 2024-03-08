using Domain.Entities;
using Domain.Interfaces;
using Services.Interfaces;

namespace Services;

public class PostService : IPostService
{
    private readonly IPostRepository storeEntity;

    public async Task<Guid> CreatePostAsync(Post entity)
    {
        if (entity.Id == Guid.Empty)
            entity = entity with { Id = Guid.NewGuid() };

        await storeEntity.AddPostAsync(entity);

        return entity.Id;
    }

    public async Task<IEnumerable<Post>> GetAllPostsAsync()
    {
        return await storeEntity.GetAllPostsAsync();
    }

    public async Task<Post> GetPostAsync(Guid entityId)
    {
        return await storeEntity.GetPostAsync(entityId);
    }
}

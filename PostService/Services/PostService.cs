using Domain.Entities;
using Domain.Interfaces;
using Services.Interfaces;

namespace Services;

public class PostService(IPostRepository postRepository, ICheckUserExist checkUserExist) : IPostService
{
    private readonly IPostRepository postRepository = postRepository;
    private readonly ICheckUserExist checkUserExist = checkUserExist;

    public async Task<Guid> CreatePostAsync(Post entity)
    {
        await checkUserExist.CheckUserExistAsync(entity.UserId);

        if (entity.Id == Guid.Empty)
            entity = entity with { Id = Guid.NewGuid() };

        await postRepository.AddPostAsync(entity);

        return entity.Id;
    }

    public async Task<IEnumerable<Post>> GetAllPostsAsync()
    {
        return await postRepository.GetAllPostsAsync();
    }

    public async Task<Post> GetPostAsync(Guid entityId)
    {
        return await postRepository.GetPostAsync(entityId);
    }
}

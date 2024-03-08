using Domain.Entities;
using Domain.Interfaces;
using System.Collections.Concurrent;

namespace Infastractes.Data;

public class PostRepository : IPostRepository
{
    private static readonly ConcurrentDictionary<Guid, Post> postsData = new();

    public async Task AddPostAsync(Post entity)
    {
        if (!postsData.ContainsKey(entity.Id))
            postsData[entity.Id] = entity;
    }

    public async Task<IEnumerable<Post>> GetAllPostsAsync()
    {
        return postsData.Values;
    }

    public async Task<Post> GetPostAsync(Guid entityId)
    {
        if (!postsData.ContainsKey(entityId))
            throw new Exception("The post is not found");

        return postsData[entityId];
    }
}
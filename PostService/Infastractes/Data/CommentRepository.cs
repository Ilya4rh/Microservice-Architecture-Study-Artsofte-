using Domain.Entities;
using Domain.Interfaces;
using System.Collections.Concurrent;

namespace Infastractes.Data;

public class CommentRepository : ICommentRepository
{
    private readonly ConcurrentDictionary<Guid, Comment> commentsData = new();

    public async Task AddCommentAsync(Comment entity)
    {
        if (!commentsData.ContainsKey(entity.Id))
            commentsData[entity.Id] = entity;
    }

    public async Task<Comment> GetCommentAsync(Guid entityId)
    {
        if (!commentsData.ContainsKey(entityId))
            throw new Exception("The comment was not found");

        return commentsData[entityId];
    }
}
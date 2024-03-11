using Domain.Entities;
using Domain.Interfaces;
using Services.Interfaces;

namespace Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository storeEntity;

    public async Task<Guid> CreateCommentAsync(Comment entity)
    {
        if (entity.Id == Guid.Empty)
            entity = entity with { Id = Guid.NewGuid() };

        await storeEntity.AddCommentAsync(entity);

        return entity.Id;
    }

    public async Task<Comment> GetCommentAsync(Guid entityId)
    {
        return await storeEntity.GetCommentAsync(entityId);
    }
}

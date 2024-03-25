using Domain.Entities;
using Domain.Interfaces;
using Services.Interfaces;

namespace Services;

public class CommentService(ICommentRepository commentRepository, ICheckUserExist checkUserExist) : ICommentService
{
    private readonly ICommentRepository commentRepository = commentRepository;
    private readonly ICheckUserExist checkUserExist = checkUserExist;

    public async Task<Guid> CreateCommentAsync(Comment entity)
    {
        await checkUserExist.CheckUserExistAsync(entity.UserId);

        if (entity.Id == Guid.Empty)
            entity = entity with { Id = Guid.NewGuid() };

        await commentRepository.AddCommentAsync(entity);

        return entity.Id;
    }

    public async Task<Comment> GetCommentAsync(Guid entityId)
    {
        return await commentRepository.GetCommentAsync(entityId);
    }
}

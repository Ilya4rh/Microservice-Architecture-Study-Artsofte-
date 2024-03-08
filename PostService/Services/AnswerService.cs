using Domain.Entities;
using Domain.Interfaces;
using Services.Interfaces;

namespace Services;

internal class AnswerService : IAnswerService
{
    private readonly IAnswerRepository storeEntity;

    public async Task<Guid> CreateAnswerAsync(Answer entity)
    {
        if (entity.Id == Guid.Empty)
            entity = entity with { Id = Guid.NewGuid() };

        await storeEntity.AddAnswerAsync(entity);

        return entity.Id;
    }

    public async Task<Answer> GetAnswerAsync(Guid entityId)
    {
        return await storeEntity.GetAnswerAsync(entityId);
    }
}
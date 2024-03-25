using Domain.Entities;
using Domain.Interfaces;
using Services.Interfaces;

namespace Services;

internal class AnswerService(IAnswerRepository answerRepository, ICheckUserExist checkUserExist) : IAnswerService
{
    private readonly IAnswerRepository answerRepository = answerRepository;
    private readonly ICheckUserExist checkUserExist = checkUserExist;

    public async Task<Guid> CreateAnswerAsync(Answer entity)
    {
        await checkUserExist.CheckUserExistAsync(entity.UserId);

        if (entity.Id == Guid.Empty)
            entity = entity with { Id = Guid.NewGuid() };

        await answerRepository.AddAnswerAsync(entity);

        return entity.Id;
    }

    public async Task<Answer> GetAnswerAsync(Guid entityId)
    {
        return await answerRepository.GetAnswerAsync(entityId);
    }
}
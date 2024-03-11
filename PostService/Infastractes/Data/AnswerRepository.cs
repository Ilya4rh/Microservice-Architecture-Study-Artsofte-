using Domain.Entities;
using Domain.Interfaces;
using System.Collections.Concurrent;

namespace Infastractes.Data;

public class AnswerRepository : IAnswerRepository
{
    private readonly ConcurrentDictionary<Guid, Answer> answersData = new();

    public async Task AddAnswerAsync(Answer entity)
    {
        if (!answersData.ContainsKey(entity.Id))
            answersData[entity.Id] = entity;
    }

    public async Task<Answer> GetAnswerAsync(Guid entityId)
    {
        if (!answersData.ContainsKey(entityId))
            throw new Exception("The answer was not found");

        return answersData[entityId];
    }
}
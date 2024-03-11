using Domain.Entities;

namespace Domain.Interfaces;

/// <summary>
/// Для внешнего взаимодействия с ответами
/// </summary>
public interface IAnswerRepository
{
    /// <summary>
    /// Возвращает один ответ
    /// </summary>
    /// <param name="entityId"> Индентификатор ответа, который нужно вернуть </param>
    /// <returns> Ответ </returns>
    Task<Answer> GetAnswerAsync(Guid entityId);

    /// <summary>
    /// Метод для добавления ответа
    /// </summary>
    /// <param name="entity"> Ответ, который нужно добавить </param>
    /// <returns> Индентификатор добавленного ответа </returns>
    Task AddAnswerAsync(Answer entity);
}

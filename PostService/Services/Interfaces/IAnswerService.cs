using Domain.Entities;

namespace Services.Interfaces;

/// <summary>
/// Бизнес-логика для сущности Answer. Включает в методы для работы с Ответами.
/// </summary>
public interface IAnswerService
{
    /// <summary>
    /// Возвращает один ответ по индексу
    /// </summary>
    /// <param name="answerId"> Индентификатор ответа, который нужно вернуть </param>
    /// <returns> Полученный по индентификатору ответ </returns>
    Task<Answer> GetAnswerAsync(Guid answerId);

    /// <summary>
    /// Метод для создания ответа
    /// </summary>
    /// <param name="answer"> Ответ, который нужно создать </param>
    /// <returns> Индентификатор созданного ответа </returns>
    Task<Guid> CreateAnswerAsync(Answer answer);
}

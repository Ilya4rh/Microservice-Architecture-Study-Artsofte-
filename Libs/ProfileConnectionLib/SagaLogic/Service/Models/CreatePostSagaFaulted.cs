namespace ProfileConnectionLib.SagaLogic.Service.Models;

public record CreatePostSagaFaulted(Guid postId, Guid userId, string Reason)
{
}
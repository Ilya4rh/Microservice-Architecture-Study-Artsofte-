namespace ProfileConnectionLib.SagaLogic.Service.Models;

public record CreatePostSagaRequest(Guid postId, Guid userId)
{
}

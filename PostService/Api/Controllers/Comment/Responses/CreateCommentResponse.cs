namespace Api.Controllers.Comment.Responses;

public record CreateCommentResponse
{
    public required Guid Id { get; init; }
}

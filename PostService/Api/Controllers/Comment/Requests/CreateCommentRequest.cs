namespace Api.Controllers.Comment.Requests;

public record CreateCommentRequest
{
    public required Guid PostId { get; init; }

    public required Guid UserId { get; init; }

    public required string Text { get; init; }
}

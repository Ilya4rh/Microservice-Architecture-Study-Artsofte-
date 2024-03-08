namespace Api.Controllers.Comment.Responses;

public record CommentInfoResponse
{
    public required Guid Id { get; init; }
    public required Guid PostId { get; init; }

    public required Guid UserId { get; init; }

    public required string Text { get; init; }
}

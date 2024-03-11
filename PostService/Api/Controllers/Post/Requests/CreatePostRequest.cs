namespace Api.Controllers.Post.Requests;

public record CreatePostRequest
{
    public required Guid UserId { get; init; }

    public required string Title { get; init; }

    public required string Description { get; init; }
}

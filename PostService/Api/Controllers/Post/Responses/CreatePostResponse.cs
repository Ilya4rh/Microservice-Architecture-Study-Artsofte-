namespace Api.Controllers.Post.Responses;

public record CreatePostResponse
{
    public required Guid Id { get; init; }
}

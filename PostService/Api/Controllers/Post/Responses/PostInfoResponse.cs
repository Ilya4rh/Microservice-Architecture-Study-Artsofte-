namespace Api.Controllers.Post.Responses;

public record PostInfoResponse
{
    public required Guid Id { get; init; }

    public required Guid UserId { get; init; }

    public required string Title { get; init; }

    public required string Description { get; init; }
}

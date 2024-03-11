namespace Api.Controllers.Answer.Responses;

public record AnswerInfoResponse
{
    public required Guid Id { get; init; }

    public required Guid UserId { get; init; }

    public required Guid CommentId { get; init; }

    public required string Text { get; init; }
}

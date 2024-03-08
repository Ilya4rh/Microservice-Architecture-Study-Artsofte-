namespace Api.Controllers.Answer.Requests;

public class CreateAnswerRequest
{
    public required Guid UserId { get; init; }

    public required Guid CommentId { get; init; }

    public required string Text { get; init; }
}

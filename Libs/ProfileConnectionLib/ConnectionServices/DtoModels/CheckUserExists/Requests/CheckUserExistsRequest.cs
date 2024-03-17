namespace ProfileConnectionLib.ConnectionServices.DtoModels.CheckUserExists.Requests;

public record CheckUserExistsRequest
{
    public required Guid Id { get; init; }
}

namespace Domain.Interfaces;

public interface ICheckUserExist
{
    Task CheckUserExistAsync(Guid userId);
}

namespace Core.Dal.Base;

public record BaseEntityDal<T>
{
    public T Id { get; init; }
}

namespace ExampleCore.Dal.Base;

public record BaseEntityDal<T>
{
    public T Id { get; init; }
}
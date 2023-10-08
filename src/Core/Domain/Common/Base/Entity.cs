namespace Domain.Common.Base;

public abstract class Entity<T> where T : notnull
{
    public T Id { get; protected set; }

    public Entity(T id)
    {
        Id = id;
    }

    public override bool Equals(object? obj)
    {
        return obj is Entity<T> entity && Id.Equals(entity.Id);
    }
}

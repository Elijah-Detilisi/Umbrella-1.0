namespace Domain.Common.Base;

public class AggregateRoot<T> : Entity<T> 
    where T : notnull
{
    public AggregateRoot(T id) : base(id)
    {
    }
}

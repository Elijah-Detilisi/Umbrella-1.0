using Domain.Common.Enums;

namespace Domain.Common.Base;

public abstract class Entity
{
    public int Id { get; protected set; }
    public DateTime ModifiedAt { get; set; }
    public DateTime CreatedAt { get; protected set; }
    public EntityStatus Status { get; protected set; }

    public Entity()
    {
        //Required for Entity framework
    }
    public Entity(int id)
    {
        Id = id;
        Status = EntityStatus.Active;
        CreatedAt = DateTime.UtcNow;
    }

    public override bool Equals(object? obj)
    {
        return obj is Entity entity && Id.Equals(entity.Id);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    
}

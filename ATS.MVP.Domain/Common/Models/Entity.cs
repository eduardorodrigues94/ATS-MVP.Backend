namespace ATS.MVP.Domain.Common.Models;

public abstract class Entity<TId> : IEquatable<Entity<TId>> where TId : notnull
{
    public TId Id { get; protected set; }

    protected Entity(TId id)
    {
        Id = id;
    }

    public override bool Equals(object? other)
    {
        return other is Entity<TId> entity && Id.Equals(entity.Id);
    }

    public static bool operator ==(Entity<TId> entity1, Entity<TId> entity2)
    {
        return Equals(entity1, entity2);
    }

    public static bool operator !=(Entity<TId> entity1, Entity<TId> entity2)
    {
        return !Equals(entity1, entity2);
    }

    public bool Equals(Entity<TId>? other)
    {
        return Equals((object?) other);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}

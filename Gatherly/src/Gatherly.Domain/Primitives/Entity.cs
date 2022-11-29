namespace Gatherly.Domain.Primitives;

public abstract class Entity : IEquatable<Entity>
{
    protected Entity(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; private init; }

    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        var item = (Entity)obj;

        return item.Id == Id;
    }
    public static bool operator ==(Entity? a, Entity? b)
    {
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.Equals(b);
    }
    public static bool operator !=(Entity? a, Entity? b)
    {
        return !(a == b);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode() * 907;
    }

    public bool Equals(Entity? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Id == other.Id;
    }
}
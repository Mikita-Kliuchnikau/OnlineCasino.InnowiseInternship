namespace GamingService.Core.Primitives;

public abstract class Entity(string id) : IEquatable<Entity>
{
    public string Id { get; } = id;

    public virtual bool Equals(Entity? other)
    {
        if (other is null)
        {
            return false;
        }
        if (other.GetType() != GetType())
        {
            return false;
        }
        return other.Id == Id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }
        if (obj.GetType() != GetType())
        {
            return false;
        }
        if (obj is not Entity other)
        {
            return false;
        }
        return other.Id == Id;
    }

    public override int GetHashCode()
    {
        ArgumentNullException.ThrowIfNull(Id);
        return Id.GetHashCode();
    }
}

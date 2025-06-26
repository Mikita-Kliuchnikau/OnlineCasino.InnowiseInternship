using MongoDB.Bson;

namespace GamingService.Core.Primitives;

public abstract class Id
{
    public abstract object Value { get; protected init; }
}

public class GuidId : Id
{
    public GuidId()  
    {
        Value = Guid.Empty;
    }

    public GuidId(Guid id)
    {
        Value = id != Guid.Empty ? id : throw new ArgumentNullException(nameof(id));
    }

    public override object Value { get; protected init; }

    public override bool Equals(object? obj)
    {
        return obj is Guid objId && (Guid)Value == objId;
    }

    public override int GetHashCode()
    {
       return Value.GetHashCode();
    }
}

public class MongoObjectId : Id
{
    public MongoObjectId() 
    { 
        Value = ObjectId.Empty;
    }

    public MongoObjectId(ObjectId id)
    {
        Value = id != ObjectId.Empty ? id : throw new ArgumentNullException(nameof(id));
    }

    public override object Value { get; protected init; }

    public override bool Equals(object? obj)
    {
        return obj is ObjectId objId && (ObjectId)Value == objId;
    }
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}
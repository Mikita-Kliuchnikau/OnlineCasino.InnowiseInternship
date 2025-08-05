using GamingService.Contracts.Abstractions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace GamingService.Contracts.Events;

public class PlayersBalancesChangedEvent : IMongoSerializable
{
    [BsonId(IdGenerator = typeof(CombGuidGenerator))]
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid PlayerId { get; init; }
    public decimal Amount { get; init; }
}

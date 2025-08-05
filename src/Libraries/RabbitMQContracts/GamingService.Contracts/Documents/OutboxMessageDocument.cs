using GamingService.Contracts.Abstractions;
using GamingService.Contracts.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace GamingService.Contracts.Documents;

public class OutboxMessageDocument<TPayload> where TPayload : IMongoSerializable
{
    [BsonId(IdGenerator = typeof(CombGuidGenerator))]
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid? MessageId { get; set; } = null;
    public DateTime OccurredAt { get; set; }
    public required TPayload Payload { get; set; }
    [BsonRepresentation(BsonType.String)]
    public Status Status { get; set; }
}

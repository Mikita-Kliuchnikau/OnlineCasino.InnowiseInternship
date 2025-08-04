using AutoMapper;
using GamingService.Core.Models.SessionAggregate;
using GamingService.Mapping.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace GamingService.DataAccess.Entities;

public class RouletteSessionDocument : IMapWith<RouletteSession>
{
    [BsonId(IdGenerator = typeof(CombGuidGenerator))]
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid Id { get; set; }
    public DateTime StartedAt { get; set; }
    [BsonRepresentation(BsonType.String)]
    public SessionStatus Status { get; set; }
    public string ServerSeed { get; set; } = string.Empty;
    public string ServerSeedHash { get; set; } = string.Empty;
    public string ClientSeed { get; set; } = string.Empty;
    public string SessionResult { get; set; } = string.Empty;
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid ConfigurationId { get; set; }
    public List<RouletteBetDocument> Bets { get; set; } = [];

    public void Mapping(Profile profile)
    {
        profile.CreateMap<RouletteSession, RouletteSessionDocument>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.StartedAt, opt => opt.MapFrom(src => src.StartedAt))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.ServerSeed, opt => opt.MapFrom(src => src.ServerSeed))
            .ForMember(dest => dest.ServerSeedHash, opt => opt.MapFrom(src => src.ServerSeedHash))
            .ForMember(dest => dest.ClientSeed, opt => opt.MapFrom(src => src.ClientSeed))
            .ForMember(dest => dest.SessionResult, opt => opt.MapFrom(src => src.SessionResult.Result))
            .ForMember(dest => dest.ConfigurationId, opt => opt.MapFrom(src => src.ConfigurationId))
            .ForMember(dest => dest.Bets, opt => opt.MapFrom(src => src.Bets));

        profile.CreateMap<RouletteSessionDocument, RouletteSession>()
            .ConstructUsing((src, context) =>
            new RouletteSession(
                src.Id,
                src.StartedAt,
                src.Status,
                src.ServerSeed,
                src.ServerSeedHash,
                src.ClientSeed,
                src.SessionResult,
                src.ConfigurationId,
                src.Bets.Select(bet => context.Mapper.Map<RouletteBet>(bet))
            ));
    }
}

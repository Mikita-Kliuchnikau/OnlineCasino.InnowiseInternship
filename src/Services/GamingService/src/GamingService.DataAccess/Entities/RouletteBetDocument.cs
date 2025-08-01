using AutoMapper;
using GamingService.Core.Common;
using GamingService.Core.Models.SessionAggregate;
using GamingService.Mapping.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace GamingService.DataAccess.Entities;

public class RouletteBetDocument : IMapWith<RouletteBet>
{
    [BsonId(IdGenerator = typeof(CombGuidGenerator))]
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid PlayerId { get; set; }
    public decimal BetAmount { get; set; }
    [BsonRepresentation(BsonType.String)]
    public Currency Currency { get; set; }
    public decimal BetWinnings { get; set; }
    [BsonRepresentation(BsonType.String)]
    public BetType BetType { get; set; }
    public List<string> BetValues { get; set; } = [];
    [BsonRepresentation(BsonType.String)]
    public BetStatus Status { get; set; }
    public List<string>? Errors { get; set; } = [];

    public void Mapping(Profile profile)
    {
        profile.CreateMap<RouletteBet, RouletteBetDocument>()
            .ForMember(dest => dest.PlayerId, opt => opt.MapFrom(src => src.PlayerId))
            .ForMember(dest => dest.BetAmount, opt => opt.MapFrom(src => src.BetAmount.Amount.Value))
            .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.BetAmount.Currency))
            .ForMember(dest => dest.BetWinnings, opt => opt.MapFrom(src => src.BetWinnings.Value))
            .ForMember(dest => dest.BetType, opt => opt.MapFrom(src => src.BetType.Name))
            .ForMember(dest => dest.BetValues, opt => opt.MapFrom(src => src.BetValues.Keys))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Errors, opt => opt.MapFrom(src => src.Errors));

        profile.CreateMap<RouletteBetDocument, RouletteBet>()
            .ConstructUsing(src =>
                RouletteBet.Create(
                    src.PlayerId,
                    new Money(src.Currency, new Amount(src.BetAmount)),
                    src.BetValues,
                    RouletteBetType.FromName(src.BetType.ToString()) ?? RouletteBetType.Default))
             .ForAllMembers(opt => opt.Ignore());
    }
}

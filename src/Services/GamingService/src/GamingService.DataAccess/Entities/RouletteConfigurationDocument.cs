using AutoMapper;
using GamingService.Core.Common;
using GamingService.Core.Extentions;
using GamingService.Core.Models.RouletteConfigurationAggregate;
using GamingService.Mapping.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace GamingService.DataAccess.Entities;

public class RouletteConfigurationDocument : IMapWith<RouletteConfiguration>
{
    [BsonId(IdGenerator = typeof(CombGuidGenerator))]
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid Id { get; set; }
    public int RouletteGameTypeValue { get; set; }
    public string RouletteGameTypeName { get; set; } = string.Empty;
    public string GameHashAlgorithm { get; set; } = string.Empty;
    [BsonRepresentation(BsonType.String)]
    public Currency Currency { get; set; }
    public decimal MinBet { get; set; }
    public decimal MaxBet { get; set; }
    public bool IsActive { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<RouletteConfiguration, RouletteConfigurationDocument>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.RouletteGameTypeValue, opt => opt.MapFrom(src => src.RouletteGameType.Value))
            .ForMember(dest => dest.RouletteGameTypeName, opt => opt.MapFrom(src => src.RouletteGameType.Name))
            .ForMember(dest => dest.GameHashAlgorithm, opt => opt.MapFrom(src => src.HashAlgorithm.GetHashAlgorithmName()))
            .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Currency))
            .ForMember(dest => dest.MinBet, opt => opt.MapFrom(src => src.MinBet.Value))
            .ForMember(dest => dest.MaxBet, opt => opt.MapFrom(src => src.MaxBet.Value))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive));

        profile.CreateMap<RouletteConfigurationDocument, RouletteConfiguration>()
            .ConstructUsing(src => 
                RouletteConfiguration.Create(
                    RouletteGameType.FromValue(src.RouletteGameTypeValue) 
                        ?? RouletteGameType.FromName(src.RouletteGameTypeName)!,
                    src.Currency,
                    new Amount(src.MinBet),
                    new Amount(src.MaxBet),
                    src.GameHashAlgorithm,
                    src.Id
                ));
    }
}

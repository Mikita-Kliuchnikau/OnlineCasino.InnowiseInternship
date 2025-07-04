using AutoMapper;
using GamingService.Application.Common.Mapping;
using GamingService.Core.Models.RouletteConfigurationAggregate;

namespace GamingService.Application.Models.Configurations.Queries.GetConfigurationDetails;

public record RouletteConfigurationVm(
    string Id,
    string RouletteType,
    string Currency,
    decimal MinBet,
    decimal MaxBet,
    string Engine) : IMapWith<RouletteConfiguration>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<RouletteConfiguration, RouletteConfigurationVm>()
            .ForMember(configVm => configVm.Id,
                opt => opt.MapFrom(config => config.Id))
            .ForMember(configVm => configVm.RouletteType,
                opt => opt.MapFrom(config => config.RouletteGameType.Name))
            .ForMember(configVm => configVm.Currency,
                opt => opt.MapFrom(config => config.Currency.ToString()))
            .ForMember(configVm => configVm.MinBet,
                opt => opt.MapFrom(config => config.MinBet.Value))
            .ForMember(configVm => configVm.MaxBet,
                opt => opt.MapFrom(config => config.MaxBet.Value))
            .ForMember(configVm => configVm.Engine,
                opt => opt.MapFrom(config => config.Engine.GetType().Name));
    }
}

using AutoMapper;
using GamingService.Application.Common.Mapping;
using GamingService.Core.Models.RouletteConfigurationAggregate;

namespace GamingService.Application.Models.Configurations.Queries.GetConfigurationDetails;

public record RouletteConfigurationViewModel(
    string Id,
    string RouletteType,
    string Currency,
    decimal MinBet,
    decimal MaxBet,
    string Engine) : IMapWith<RouletteConfiguration>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<RouletteConfiguration, RouletteConfigurationViewModel>()
            .ForMember(configViewModel => configViewModel.Id,
                opt => opt.MapFrom(config => config.Id))
            .ForMember(configViewModel => configViewModel.RouletteType,
                opt => opt.MapFrom(config => config.RouletteGameType.Name))
            .ForMember(configViewModel => configViewModel.Currency,
                opt => opt.MapFrom(config => config.Currency.ToString()))
            .ForMember(configViewModel => configViewModel.MinBet,
                opt => opt.MapFrom(config => config.MinBet.Value))
            .ForMember(configViewModel => configViewModel.MaxBet,
                opt => opt.MapFrom(config => config.MaxBet.Value))
            .ForMember(configViewModel => configViewModel.Engine,
                opt => opt.MapFrom(config => config.Engine.GetType().Name));
    }
}

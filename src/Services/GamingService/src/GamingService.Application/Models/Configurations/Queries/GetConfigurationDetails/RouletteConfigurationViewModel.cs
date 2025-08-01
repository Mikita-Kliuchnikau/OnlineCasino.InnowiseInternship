using AutoMapper;
using GamingService.Application.Common.Mapping;
using GamingService.Core.Common;
using GamingService.Core.Extentions;
using GamingService.Core.Models.RouletteConfigurationAggregate;

namespace GamingService.Application.Models.Configurations.Queries.GetConfigurationDetails;

public class RouletteConfigurationViewModel : IMapWith<RouletteConfiguration>
{
    public Guid Id { get; set; }
    public string RouletteType { get; set; } = string.Empty;
    public Currency Currency { get; set; }
    public Amount MinBet { get; set; } = new(0);
    public Amount MaxBet { get; set; } = new(0);
    public string HashAlgorithm { get; set; } = string.Empty;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<RouletteConfiguration, RouletteConfigurationViewModel>()
            .ForMember(configViewModel => configViewModel.Id,
                opt => opt.MapFrom(config => config.Id))
            .ForMember(configViewModel => configViewModel.RouletteType,
                opt => opt.MapFrom(config => config.RouletteGameType.Name))
            .ForMember(configViewModel => configViewModel.Currency,
                opt => opt.MapFrom(config => config.Currency))
            .ForMember(configViewModel => configViewModel.MinBet,
                opt => opt.MapFrom(config => config.MinBet))
            .ForMember(configViewModel => configViewModel.MaxBet,
                opt => opt.MapFrom(config => config.MaxBet))
            .ForMember(configViewModel => configViewModel.HashAlgorithm,
                opt => opt.MapFrom(config => config.HashAlgorithm.GetHashAlgorithmName()));
    }
}

using AutoMapper;
using GamingService.Application.Common.Mapping;
using GamingService.Application.Models.Configurations.Queries.GetConfigurationDetails;
using GamingService.Core.Common;
using GamingService.Core.Models.RouletteConfigurationAggregate;
using MediatR;
using static GamingService.Core.Constants.ErrorMessages;

namespace GamingService.Application.Models.Configurations.Commands.CreateConfiguration;

public class CreateRouletteConfigurationCommand : IRequest<RouletteConfigurationViewModel>, IMapWith<RouletteConfiguration>
{
    public RouletteType RouletteType { get; set; }
    public Currency Currency { get; set; }
    public Amount MinBet { get; set; } = new(0);
    public Amount MaxBet { get; set; } = new(0);
    public string? HashAlgorithm { get; set; }

    public void Mapping(Profile profile)
    {
        var gameType = RouletteGameType.FromName(RouletteType.ToString())
            ?? throw new ArgumentException(string.Format(RouletteTypeNotFound, RouletteType));

        profile.CreateMap<CreateRouletteConfigurationCommand, RouletteConfiguration>()
            .ConvertUsing(src =>
                RouletteConfiguration.Create(
                    gameType,
                    src.Currency,
                    src.MinBet,
                    src.MaxBet,
                    src.HashAlgorithm!,
                    null));
    }
}

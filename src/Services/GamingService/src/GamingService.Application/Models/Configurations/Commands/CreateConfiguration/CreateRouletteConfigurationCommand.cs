using GamingService.Application.Models.Configurations.Queries.GetConfigurationDetails;
using MediatR;
using System.Security.Cryptography;

namespace GamingService.Application.Models.Configurations.Commands.CreateConfiguration;

public record CreateRouletteConfigurationCommand(
    string RouletteType,
    string Currency,
    decimal MinBet,
    decimal MaxBet,
    HashAlgorithm? Engine = null) : IRequest<RouletteConfigurationVm>;

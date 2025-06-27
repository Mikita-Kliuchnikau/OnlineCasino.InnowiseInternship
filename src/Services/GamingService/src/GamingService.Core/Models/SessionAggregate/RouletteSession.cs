using GamingService.Core.Contracts;
using GamingService.Core.Events;
using GamingService.Core.Models.RouletteConfigurationAggregate;
using GamingService.Core.Primitives;
using MediatR;
using System.Security.Cryptography;
using System.Text;
using static GamingService.Core.Constants.ErrorMessages;

namespace GamingService.Core.Models.SessionAggregate;

public class RouletteSession : Entity
{
    private const int SEED_BYTE_LENGTH = 32;

    private readonly IMediator _mediator;

    private readonly List<RouletteBet> _bets = [];

    private RouletteSession(
        Id id,
        string serverSeed,
        string serverSeedHash,
        string clientSeed,
        RouletteSpinResult sessionResult,
        RouletteConfiguration configuration,
        IMediator mediator) : base(id)
    {
        ServerSeed = serverSeed;
        ServerSeedHash = serverSeedHash;
        ClientSeed = clientSeed;
        SessionResult = sessionResult;
        Configuration = configuration;
        _mediator = mediator;
    }

    public DateTime StartedAt { get; private init; } = DateTime.UtcNow;

    public SessionStatus Status { get; private set; } = SessionStatus.Pending;

    public string ServerSeed { get; private init; } 

    public string ServerSeedHash { get; private init; }

    public string ClientSeed { get; private init; }

    public RouletteSpinResult SessionResult { get; private init; } 

    public RouletteConfiguration Configuration { get; private init; }

    public IReadOnlyList<RouletteBet>? Bets => _bets;

    public static RouletteSession Create(Id id, string clientSeed, RouletteConfiguration configuration, IMediator mediator)
    {
        var serverSeed = GenerateSeed();
        var serverSeedHash = GenerateSeedHash(serverSeed);
        if (string.IsNullOrWhiteSpace(clientSeed))
        {
            clientSeed = GenerateSeed();
        }
        var source = $"{serverSeed}:{clientSeed}";
        var sessionResult = new RouletteSpinResult(source, configuration);
        return new RouletteSession(id, serverSeed, serverSeedHash, clientSeed, sessionResult, configuration, mediator);
    }

    public async Task<RouletteSession> CloseSession(IEnumerable<RouletteBet> bets)
    {
        foreach (var bet in bets)
        {
            ArgumentNullException.ThrowIfNull(bet);

            if (bet.Errors?.Count != 0)
            {
                bet.ChangeStatus(BetStatus.Cancelled);
                continue;
            }
            if (bet.Player.Balance.Currency != bet.BetAmount.Currency)
            {
                bet.ChangeStatus(BetStatus.Cancelled);
                bet.AddErrors(BetCurrencyMismatch);
                continue;
            }
            bet.Player.Balance.Amount.Value -= bet.BetAmount.Amount.Value;

            if (bet.Player.Balance.Amount.Value < 0)
            {
                bet.ChangeStatus(BetStatus.Cancelled);
                bet.AddErrors(PlayerBalanceInsufficient);
                continue;
            }
            if (bet.BetAmount.Amount.Value > Configuration.MaxBet.Value)
            {
                bet.ChangeStatus(BetStatus.Cancelled);
                bet.AddErrors(BetAmountExceedsMaxBet);
                continue;
            }
            if (bet.BetAmount.Amount.Value < Configuration.MinBet.Value)
            {
                bet.ChangeStatus(BetStatus.Cancelled);
                bet.AddErrors(BetAmountBelowMinBet);
                continue;
            }
            if (bet.BetType == RouletteBetType.Basket && Configuration.RouletteGameType == RouletteGameType.European)
            {
                bet.ChangeStatus(BetStatus.Cancelled);
                bet.AddErrors(BasketBetNotAllowed);
                continue;
            }

            if (bet.BetValues.Keys!.Contains("00") && Configuration.RouletteGameType == RouletteGameType.European)
            {
                bet.ChangeStatus(BetStatus.Cancelled);
                bet.AddErrors(BetValuesCannotContain00InEuropean);
                continue;
            }

            if (bet.Status == BetStatus.Pending)
            {
                var betStatus = bet.BetValues.Keys!.Contains(SessionResult.ToString())
                    ? BetStatus.Won
                    : BetStatus.Lost;
                bet.ChangeStatus(betStatus);
            }
        }
        _bets.AddRange(bets);

        Status = SessionStatus.Closed;

        var changes = _bets?
            .Where(BetType => BetType.Status == BetStatus.Won || BetType.Status == BetStatus.Lost)
            .Select(bet => new PlayerBalanceChange(
                bet.Player.Id,
                bet.BetAmount.Currency,
                bet.Status switch
                {
                    BetStatus.Lost => -bet.BetAmount.Amount.Value,
                    BetStatus.Won => bet.BetAmount.Amount.Value * bet.BetType.WinningsMultiplier,
                    _ => 0
                }))
            .ToList();

        if (changes != null && changes.Count == 0)
        {
            var playersBalancesChangesEvent = new PlayersBalancesChangedDomainEvent(changes);

            ArgumentNullException.ThrowIfNull(_mediator);
            await _mediator.Publish(playersBalancesChangesEvent);
        }
        return this;
    }

    private static string GenerateSeed()
    {
        var bytes = new byte[SEED_BYTE_LENGTH];
        RandomNumberGenerator.Fill(bytes);
        return Convert.ToHexString(bytes);
    }

    private static string GenerateSeedHash(string seed)
    {
        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(seed));
        return Convert.ToHexString(hash);
    }
}

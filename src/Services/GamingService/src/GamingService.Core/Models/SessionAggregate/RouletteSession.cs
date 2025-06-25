using GamingService.Core.Models.RouletteConfigurationAggregate;
using GamingService.Core.Primitives;
using MongoDB.Bson;
using System.Security.Cryptography;
using System.Text;

namespace GamingService.Core.Models.SessionAggregate;

public class RouletteSession : Entity
{
    private const int SEED_BYTE_LENGTH = 32;

    private readonly List<RouletteBet> _bets = [];

    private RouletteSession(
        string serverSeed,
        string serverSeedHash,
        string clientSeed,
        string sessionResult,
        RouletteConfiguration configuration)
    {
        ServerSeed = serverSeed;
        ServerSeedHash = serverSeedHash;
        ClientSeed = clientSeed;
        SessionResult = sessionResult;
        Configuration = configuration;
    }

    public DateTime StartedAt { get; private init; } = DateTime.UtcNow;

    public SessionStatus Status { get; private set; } = SessionStatus.Pending;

    public string ServerSeed { get; private init; } 

    public string ServerSeedHash { get; private init; }

    public string ClientSeed { get; private init; }

    public string SessionResult { get; private init; } 

    public RouletteConfiguration Configuration { get; private init; }

    public IReadOnlyList<RouletteBet>? Bets => _bets;

    public static RouletteSession Create(string clientSeed, RouletteConfiguration configuration)
    {
        var serverSeed = GenerateSeed();
        var serverSeedHash = GenerateSeedHash(serverSeed);
        if (string.IsNullOrWhiteSpace(clientSeed))
        {
            clientSeed = GenerateSeed();
        }
        var sessionResult = ComputeResult(serverSeed, clientSeed, configuration);
        return new RouletteSession(serverSeed, serverSeedHash, clientSeed, sessionResult, configuration);
    }

    public string CloseSession(IEnumerable<RouletteBet> bets)
    {
        foreach (var bet in bets)
        {
            ArgumentNullException.ThrowIfNull(bet);

            if (bet.Player.Balance.Currency != bet.BetAmount.Currency)
            {
                bet.ChangeStatus(BetStatus.Cancelled);
                continue;
            }
            bet.Player.Balance.Amount.Value -= bet.BetAmount.Amount.Value;
            
            if (bet.Player.Balance.Amount.Value < 0)
            {
                bet.ChangeStatus(BetStatus.Cancelled);
                continue;
            }
            if (bet.BetAmount.Amount.Value > Configuration.MaxBet.Value)
            {
                bet.ChangeStatus(BetStatus.Cancelled);  
                continue;
            }
            if (bet.BetAmount.Amount.Value < Configuration.MinBet.Value)
            {
                bet.ChangeStatus(BetStatus.Cancelled);
                continue;
            }
            if (bet.BetType == RouletteBetType.Basket && Configuration.RouletteGameType == RouletteGameType.European)
            {
                bet.ChangeStatus(BetStatus.Cancelled);
                continue;
            }

            if (bet.BetValues.Keys!.Contains("00") && Configuration.RouletteGameType == RouletteGameType.European)
            {
                bet.ChangeStatus(BetStatus.Cancelled);
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

        return Id;
    }

    private static string ComputeResult(string serverSeed, string clientSeed, RouletteConfiguration configuration)
    {
        var input = $"{serverSeed}:{clientSeed}";
        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(input));
        int number = BitConverter.ToInt32(hash, 0);
        var result = Math.Abs(number % configuration.RouletteGameType.NumberOfPossibleBets);
        return result == RouletteGameType.American.NumberOfPossibleBets
            ? "00"
            : result.ToString();
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

using GamingService.Core.Models.RouletteConfigurationAggregate;
using GamingService.Core.Primitives;
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
        Nonce nonce,
        int sessionResult,
        RouletteConfiguration configuration) : base(Guid.NewGuid())
    {
        ServerSeed = serverSeed;
        ServerSeedHash = serverSeedHash;
        ClientSeed = clientSeed;
        Nonce = nonce;
        SessionResult = sessionResult;
        Configuration = configuration;
    }

    public DateTime StartedAt { get; private init; } = DateTime.UtcNow;

    public SessionStatus Status { get; private set; } = SessionStatus.Pending;

    public string ServerSeed { get; private init; } 

    public string ServerSeedHash { get; private init; }

    public string ClientSeed { get; private init; }

    public Nonce Nonce { get; private init; }

    public int SessionResult { get; private init; } 

    public RouletteConfiguration Configuration { get; private init; }

    public IReadOnlyList<RouletteBet>? Bets => _bets;

    public static RouletteSession Create(string clientSeed, Nonce nonce, RouletteConfiguration configuration)
    {
        var serverSeed = GenerateSeed();
        var serverSeedHash = GenerateSeedHash(serverSeed);
        if (string.IsNullOrWhiteSpace(clientSeed))
        {
            clientSeed = GenerateSeed();
        }
        var sessionResult = ComputeResult(serverSeed, clientSeed, nonce);
        return new RouletteSession(serverSeed, serverSeedHash, clientSeed, nonce, sessionResult, configuration);
    }

    public Guid CloseSession(IEnumerable<RouletteBet> bets)
    {
        foreach (var bet in bets)
        {
            ArgumentNullException.ThrowIfNull(bet);

            if (bet.BetType == RouletteBetType.Basket && Configuration.RouletteGameType == RouletteGameType.EuropeanRoulette)
            {
                bet.Status = BetStatus.Cancelled;
            }

            if (bet.BetValues.Keys!.Contains("00") && Configuration.RouletteGameType == RouletteGameType.EuropeanRoulette)
            {
                bet.Status = BetStatus.Cancelled;
            }

            if (bet.Status == BetStatus.Pending)
            {
                bet.Status = bet.BetValues.Keys!.Contains(SessionResult.ToString())
                    ? BetStatus.Won
                    : BetStatus.Lost;
            }
        }
        _bets.AddRange(bets);

        Status = SessionStatus.Closed;

        return Id;
    }

    private static int ComputeResult(string serverSeed, string clientSeed, Nonce nonce)
    {
        var input = $"{serverSeed}:{clientSeed}:{nonce}";
        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(input));
        int number = BitConverter.ToInt32(hash, 0);
        return Math.Abs(number % 37);
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

public record Nonce(int Value)
{
    public static Nonce Create(int value)
    {
        return value < 0 ? throw new ArgumentOutOfRangeException(nameof(value)) : new Nonce(value);
    }
    public override string ToString() => Value.ToString();
}

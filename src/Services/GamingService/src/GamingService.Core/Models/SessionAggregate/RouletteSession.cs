using GamingService.Core.Abstractions;
using GamingService.Core.Primitives;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
[assembly: InternalsVisibleTo("GamingService.DataAccess")]

namespace GamingService.Core.Models.SessionAggregate;

public class RouletteSession : Entity
{
    private const int SeedByteLength = 32;

    private readonly List<RouletteBet> _bets = [];

    private RouletteSession(
        string serverSeed,
        string serverSeedHash,
        string clientSeed,
        RouletteSpinResult sessionResult,
        Guid configurationId,
        Guid? id) : base(id)
    {
        ServerSeed = serverSeed;
        ServerSeedHash = serverSeedHash;
        ClientSeed = clientSeed;
        SessionResult = sessionResult;
        ConfigurationId = configurationId;
    }

    internal RouletteSession(
        Guid id,
        DateTime startedAt,
        SessionStatus status,
        string serverSeed,
        string serverSeedHash,
        string clientSeed,
        string sessionResult,
        Guid configurationId,
        IEnumerable<RouletteBet> bets) : base(id)
    {
        StartedAt = startedAt;
        Status = status;
        ServerSeed = serverSeed;
        ServerSeedHash = serverSeedHash;
        ClientSeed = clientSeed;
        SessionResult = new RouletteSpinResult(sessionResult);
        ConfigurationId = configurationId;
        _bets.AddRange(bets);
    }

    public DateTime StartedAt { get; private init; } = DateTime.UtcNow;

    public SessionStatus Status { get; private set; } = SessionStatus.Pending;

    public string ServerSeed { get; private init; } 

    public string ServerSeedHash { get; private init; }

    public string ClientSeed { get; private init; }

    public RouletteSpinResult SessionResult { get; private init; } 

    public Guid ConfigurationId { get; private set; }

    public IReadOnlyList<RouletteBet>? Bets => _bets;

    public static async Task<RouletteSession> Create(
        string clientSeed,
        Guid configurationId,
        IRouletteConfiguratonsRepository configuratonsRepository,
        Guid? id = null)
    {
        var serverSeed = GenerateSeed();
        var serverSeedHash = GenerateSeedHash(serverSeed);
        if (string.IsNullOrWhiteSpace(clientSeed))
        {
            clientSeed = GenerateSeed();
        }
        var source = $"{serverSeed}:{clientSeed}";

        var configuration = await configuratonsRepository.GetByIdAsync(configurationId);
        var sessionResult = new RouletteSpinResult(source, configuration);
        return new RouletteSession(serverSeed, serverSeedHash, clientSeed, sessionResult, configurationId, id);
    }

    public async Task<RouletteSession> CloseSession(
        IEnumerable<RouletteBet> bets, 
        IPlayersRepository playersRepository,
        IRouletteConfiguratonsRepository configuratonsRepository)
    {
        var configuration = await configuratonsRepository.GetByIdAsync(ConfigurationId);
        await bets.Validate(configuration, SessionResult.Result, playersRepository);

        _bets.AddRange(bets);

        Status = SessionStatus.Closed;

        return this;
    }

    private static string GenerateSeed()
    {
        var bytes = new byte[SeedByteLength];
        RandomNumberGenerator.Fill(bytes);
        return Convert.ToHexString(bytes);
    }

    private static string GenerateSeedHash(string seed)
    {
        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(seed));
        return Convert.ToHexString(hash);
    }
}

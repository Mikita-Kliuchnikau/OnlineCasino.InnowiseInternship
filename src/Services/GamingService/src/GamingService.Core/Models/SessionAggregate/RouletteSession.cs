using GamingService.Core.Events;
using GamingService.Core.Models.RouletteConfigurationAggregate;
using GamingService.Core.Primitives;
using MediatR;
using System.Security.Cryptography;
using System.Text;

namespace GamingService.Core.Models.SessionAggregate;

public class RouletteSession : Entity
{
    private const int SeedByteLength = 32;

    private readonly IMediator _mediator;

    private readonly List<RouletteBet> _bets = [];

    private RouletteSession(
        string id,
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

    public static RouletteSession Create(string id, string clientSeed, RouletteConfiguration configuration, IMediator mediator)
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
        bets.Validate(Configuration, SessionResult.Result);

        _bets.AddRange(bets);

        Status = SessionStatus.Closed;

        ArgumentNullException.ThrowIfNull(_mediator);
        await _mediator.Publish(new PlayersBalancesChangedDomainEvent(this.Id));
        
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

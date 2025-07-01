using GamingService.Core.Models.RouletteConfigurationAggregate;
using System.Security.Cryptography;
using System.Text;

namespace GamingService.Core.Models.SessionAggregate;

public class RouletteSpinResult
{
    public RouletteSpinResult(string source, RouletteConfiguration configuration)
    {
        var engine = configuration.Engine ?? SHA256.Create();
        var hash = engine.ComputeHash(Encoding.UTF8.GetBytes(source));
        int number = BitConverter.ToInt32(hash, 0);
        var result = Math.Abs(number % configuration.RouletteGameType.NumberOfPossibleBets);
        Result = result == RouletteGameType.American.NumberOfPossibleBets 
            ? "00" 
            : result.ToString("D2");
    }

    public string Result { get; }
}

namespace GamingService.DataAccess.Options;

public class GrpcOptions
{
    public string ServerUrl { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public string ApiKeyHeaderName { get; set; } = string.Empty;
    public int MaxAttempts { get; set; }
    public TimeSpan InitialBackoff { get; set; }
    public TimeSpan MaxBackoff { get; set; }
    public double BackoffMultiplier { get; set; }
    public TimeSpan Timeout { get; set; }
}

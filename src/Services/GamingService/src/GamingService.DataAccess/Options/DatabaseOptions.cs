namespace GamingService.DataAccess.Options;

public class DatabaseOptions
{
    public string HostName { get; set; } = string.Empty;
    public int Port { get; set; } = 27017;
    public string ReplicaSetName { get; set; } = string.Empty;
    public string CredentialName { get; set; } = string.Empty;
    public string CredentialUser { get; set; } = string.Empty;
    public string CredentialPassword { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;
    public string SessionsCollection { get; set; } = string.Empty;
    public string GamesCollection { get; set; } = string.Empty;
    public string OutboxCollection { get; set; } = string.Empty;
}

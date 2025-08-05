namespace GamingService.OutboxWorker.Options;

public class DatabaseOptions
{
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; } = 27017;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ReplicaSet { get; set; } = string.Empty;
    public string CredentialSource { get; set; } = string.Empty;
    public string ConnectionString { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;
    public string OutboxCollectionName { get; set; } = string.Empty;
}

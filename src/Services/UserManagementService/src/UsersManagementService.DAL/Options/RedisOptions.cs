namespace UsersManagementService.DAL.Options;

public class RedisOptions
{
    public string ConnectionString { get; set; } = string.Empty;
    public string PrefixKey { get; set; } = string.Empty;
    public TimeSpan MessageExpiry { get; set; }
}

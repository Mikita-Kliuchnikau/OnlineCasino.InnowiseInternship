namespace UsersManagementService.DAL.Options;

public class DatabaseOptions
{
    public string ConnectionString { get; set; } = string.Empty;

    public int MaxRetryCount { get; set; }

    public int CommandTimeOut { get; set; }
}
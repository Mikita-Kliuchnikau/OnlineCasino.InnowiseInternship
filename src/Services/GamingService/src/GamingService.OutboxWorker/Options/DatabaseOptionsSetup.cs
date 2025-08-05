using Microsoft.Extensions.Options;
using static GamingService.OutboxWorker.Constants.DatabaseConstants;

namespace GamingService.OutboxWorker.Options;

public class DatabaseOptionsSetup(IConfiguration configuration)
    : IConfigureOptions<DatabaseOptions>
{
    public void Configure(DatabaseOptions options)
    {
        configuration.GetSection(ConfigurationSectionName).Bind(options);
    }
}

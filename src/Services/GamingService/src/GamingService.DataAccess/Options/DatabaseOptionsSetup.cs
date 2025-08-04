using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using static GamingService.DataAccess.Constants.DatabaseConstants;

namespace GamingService.DataAccess.Options;

public class DatabaseOptionsSetup(IConfiguration configuration)
    : IConfigureOptions<DatabaseOptions>
{
    public void Configure(DatabaseOptions options)
    {
        configuration.GetSection(ConfigurationSectionName).Bind(options);
    }
}

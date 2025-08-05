using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using static UsersManagementService.Presentation.Constants.RedisConstants;

namespace UsersManagementService.DAL.Options;

public class RedisOptionsSetup(IConfiguration configuration) : IConfigureOptions<RedisOptions>
{
    public void Configure(RedisOptions options)
    {
        configuration.GetConnectionString(RedisConnectionStringName);
        configuration.GetSection(ConfigurationSectionName).Bind(options);
    }
}

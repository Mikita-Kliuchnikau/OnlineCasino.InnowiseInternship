using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using static GamingService.DataAccess.Constants.GrpcConstants;

namespace GamingService.DataAccess.Options;

public class GrpcOptionsSetup(IConfiguration configuration)
    : IConfigureOptions<GrpcOptions>
{
    public void Configure(GrpcOptions options)
    {
        configuration.GetSection(ConfigurationSectionName).Bind(options);
        configuration.GetSection(UserManagementServiceApiKeyConfigurationSectionName)
            .Bind(options);
        configuration.GetSection(RetryPolicyConfigurationSectionName).Bind(options);
    }
}

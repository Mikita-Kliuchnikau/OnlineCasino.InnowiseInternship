using Microsoft.Extensions.Options;
using static UsersManagementService.Presentation.Constants.GrpcConstants;

namespace UsersManagementService.Presentation.Options;

public class GrpcOptionsSetup(IConfiguration configuration)
    : IConfigureOptions<GrpcOptions>
{
    public void Configure(GrpcOptions options)
    {
        configuration.GetSection(ConfigurationSectionName).Bind(options);
    }
}

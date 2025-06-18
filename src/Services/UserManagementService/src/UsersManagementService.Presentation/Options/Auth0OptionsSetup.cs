using Microsoft.Extensions.Options;
using static UsersManagementService.Presentation.Constants.AuthConstants;

namespace UsersManagementService.Presentation.Options;

public class Auth0OptionsSetup(IConfiguration configuration)
    : IConfigureOptions<Auth0Options>
{
    public void Configure(Auth0Options options)
    {
        configuration.GetSection(ConfigurationSectionName).Bind(options);
    }
}

using Microsoft.Extensions.Options;
using static GamingService.Presentation.Constants.AuthConstants;

namespace GamingService.Presentation.Options;

public class Auth0OptionsSetup(IConfiguration configuration)
    : IConfigureOptions<Auth0Options>
{
    public void Configure(Auth0Options options)
    {
        configuration.GetSection(ConfigurationSectionName).Bind(options);
    }
}

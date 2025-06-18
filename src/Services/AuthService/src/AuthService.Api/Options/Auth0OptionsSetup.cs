using Microsoft.Extensions.Options;
using static AuthService.Api.Constants.ConfigurationConstans;

namespace AuthService.Api.Options;

public class Auth0OptionsSetup(IConfiguration configuration)
    : IConfigureOptions<Auth0Options>
{
    public void Configure(Auth0Options options)
    {
        configuration.GetSection(ConfigurationSectionName).Bind(options);
    }
}

using Auth0.AspNetCore.Authentication;
using AuthService.Api.Options;
using Microsoft.Extensions.Options;

namespace AuthService.Api.Extensions;

public static class DIExtension
{
    public static IServiceCollection AddAuth0Authentication(this IServiceCollection services)
    {
        var authOptions = services
            .BuildServiceProvider()
            .GetRequiredService<IOptions<Auth0Options>>().Value;

        services.AddAuth0WebAppAuthentication(options =>
        {
            options.Domain = authOptions.Domain;
            options.ClientId = authOptions.ClientId;
            options.ClientSecret = authOptions.ClientSecret;
        });
        return services;
    }
}

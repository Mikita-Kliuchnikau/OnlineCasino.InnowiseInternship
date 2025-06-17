using Auth0.AspNetCore.Authentication;
using AuthService.Api.Options;
using Microsoft.Extensions.Options;

namespace AuthService.Api.Extensions;

public static class DIExtension
{
    public static IServiceCollection AddAuth0Authentication(this IServiceCollection services,
        IOptions<Auth0Options> authOptions)
    {
        services.AddAuth0WebAppAuthentication(options =>
        {
            options.Domain = authOptions.Value.Domain;
            options.ClientId = authOptions.Value.ClientId;
            options.ClientSecret = authOptions.Value.ClientSecret;
        });
        return services;
    }
}

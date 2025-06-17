using Microsoft.AspNetCore.Authentication;
using static AuthService.Api.Constants.UrlConstants;
using static AuthService.Api.Constants.Common;
using AuthService.Api.Extensions;
using AuthService.Api.Options;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureOptions<Auth0OptionsSetup>();

builder.Services.AddAuth0Authentication(Options.Create(new Auth0Options()));

builder.Services.AddHttpClient();

var app = builder.Build();

app.UseAuthentication();

app.MapGet("/login", (HttpContext httpContext, IOptions<Auth0Options> options, string redirectUrl) =>
{
    var domain = options.Value.Domain;
    var clientId = options.Value.ClientId;
    var responseType = ResponseType;
    var scope = RequestScope;

    var loginUrl = string.Format(BaseAuthorizeUrl, domain,
        Uri.EscapeDataString(clientId),
        Uri.EscapeDataString(redirectUrl),
        Uri.EscapeDataString(responseType),
        Uri.EscapeDataString(scope));

    return Results.Ok(loginUrl);
});

app.MapGet("/signup", (HttpContext httpContext, IOptions<Auth0Options> options, string redirectUrl) =>
{
    var domain = options.Value.Domain;
    var clientId = options.Value.ClientId;
    var responseType = ResponseType;
    var scope = RequestScope;

    var baseUrl = string.Format(BaseAuthorizeUrl, domain,
        Uri.EscapeDataString(clientId),
        Uri.EscapeDataString(redirectUrl),
        Uri.EscapeDataString(responseType),
        Uri.EscapeDataString(scope));
    var signupUrl = baseUrl + SignupHint;

    return Results.Ok(signupUrl);
});

app.MapGet("/logout", async (HttpContext httpContext, IOptions<Auth0Options> options, string redirectUrl) =>
{
    await httpContext.SignOutAsync();

    var domain = options.Value.Domain;
    var clientId = options.Value.ClientId;

    var logoutUrl = string.Format(BaseLogoutUrl, domain,
        Uri.EscapeDataString(clientId),
        Uri.EscapeDataString(redirectUrl));

    return Results.Ok(logoutUrl);
});

app.Run();

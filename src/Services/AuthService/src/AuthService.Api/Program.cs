using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using static AuthService.Api.Constants.ConfigurationConstans;
using static AuthService.Api.Constants.UrlConstants;
using static AuthService.Api.Constants.Common;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();

var _domain = builder.Configuration.GetSection(ConfigurationSectionName).GetSection(DomainKey).Value!;
var _clientId = builder.Configuration.GetSection(ConfigurationSectionName).GetSection(ClientIdKey).Value!;
var _clientSecret = builder.Configuration.GetSection(ConfigurationSectionName).GetSection(ClientSecretKey).Value!;

builder.Services.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = _domain;
    options.ClientId = _clientId;
    options.ClientSecret = _clientSecret;
});

builder.Services.AddHttpClient();

var app = builder.Build();

app.UseAuthentication();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/login", (HttpContext httpContext, string redirectUrl = "https://localhost:7041") =>
{
    var domain = _domain;
    var clientId = _clientId;
    var responseType = ResponseType;
    var scope = RequestScope;

    var loginUrl = string.Format(BaseAuthorizeUrl, domain,
        Uri.EscapeDataString(clientId),
        Uri.EscapeDataString(redirectUrl),
        Uri.EscapeDataString(responseType),
        Uri.EscapeDataString(scope));

    return Results.Ok(loginUrl);
});

app.MapGet("/signup", (HttpContext httpContext, string redirectUrl = "https://localhost:7041") =>
{
    var domain = _domain;
    var clientId = _clientId;
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

app.MapGet("/logout", async (HttpContext httpContext, string redirectUrl) =>
{
    await httpContext.SignOutAsync();

    var domain = _domain;
    var clientId = _clientId;

    var logoutUrl = string.Format(BaseLogoutUrl, domain,
        Uri.EscapeDataString(clientId),
        Uri.EscapeDataString(redirectUrl));

    return Results.Ok(logoutUrl);
});

app.Run();

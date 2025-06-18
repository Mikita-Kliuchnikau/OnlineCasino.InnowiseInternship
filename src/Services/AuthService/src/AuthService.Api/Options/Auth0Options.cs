namespace AuthService.Api.Options;

public class Auth0Options
{
    public string Domain { get; set; } = string.Empty;

    public string ClientId { get; set; } = string.Empty;

    public string ClientSecret { get; set; } = string.Empty;
}

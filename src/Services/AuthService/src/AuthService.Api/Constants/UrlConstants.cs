namespace AuthService.Api.Constants;

public static class UrlConstants
{
    public static readonly string BaseAuthorizeUrl = "https://{0}/authorize" +
                                                     "?client_id={1}" +
                                                     "&redirect_uri={2}" +
                                                     "&response_type={3}" +
                                                     "&scope={4}";
    public static readonly string BaseLogoutUrl = "https://{0}/v2/logout" +
                                                  "?client_id={1}" +
                                                  "&returnTo={2}";
}

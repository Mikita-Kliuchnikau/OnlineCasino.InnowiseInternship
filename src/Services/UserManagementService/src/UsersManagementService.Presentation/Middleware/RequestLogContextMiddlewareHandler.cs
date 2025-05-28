namespace UsersManagementService.Presentation.Middleware;

public static class RequestLogContextMiddlewareHandler
{
    public static IApplicationBuilder UseRequestLogContextMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestLogContextMiddleware>();
    }
}

namespace UsersManagmentService.Presentation.Middleware;

public static class ExceptionMiddlewareHandler
{
    public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionMiddleware>();
    }
}

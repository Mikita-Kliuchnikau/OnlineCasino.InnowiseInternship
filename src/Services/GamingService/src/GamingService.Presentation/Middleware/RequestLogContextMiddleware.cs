using Serilog.Context;

namespace GamingService.Presentation.Middleware;

public class RequestLogContextMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        using (LogContext.PushProperty("CorrelationId", context.TraceIdentifier))
        {
            await next(context);
        }
    }
}

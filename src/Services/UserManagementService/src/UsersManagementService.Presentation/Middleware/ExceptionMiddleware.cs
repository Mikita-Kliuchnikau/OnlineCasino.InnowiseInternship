using Microsoft.AspNetCore.Mvc;
using Serilog.Context;
using System.Net;
using static UsersManagementService.Presentation.Constants.MediaTypeConstants;

namespace UsersManagementService.Presentation.Middleware;

public class ExceptionMiddleware(
    RequestDelegate next,
    ILogger<ExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception ex)
        {
            using (LogContext.PushProperty("Error", ex.Message, true))
            {
                logger.LogError(
                    ex,
                    "Request {@Request} complited with error",
                    context.Request.Path);
            }

            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var (statusCode, message) = ex switch
        {
            _ => (HttpStatusCode.InternalServerError, ex.Message)
        };

        ProblemDetails details = new()
        {
            Status = (int)statusCode,
            Title = message,
        };

        context.Response.ContentType = Json;
        context.Response.StatusCode = (int)statusCode;
        
        await context.Response.WriteAsJsonAsync(details);
    }
}
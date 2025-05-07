using Microsoft.AspNetCore.Mvc;
using System.Net;
using UsersManagmentService.Presentation.Constants;

namespace UsersManagmentService.Presentation.Middleware;

public class ExceptionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception ex)
        {
           await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
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

        context.Response.ContentType = ExceptionConstants.ExceptionOptions;
        context.Response.StatusCode = (int)statusCode;
        
        await context.Response.WriteAsJsonAsync(details);
    }
}

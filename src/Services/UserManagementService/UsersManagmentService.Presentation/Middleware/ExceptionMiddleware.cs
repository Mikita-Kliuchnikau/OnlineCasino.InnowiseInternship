using System.ComponentModel.DataAnnotations;
using System.Net;

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

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;
        
        await context.Response.WriteAsJsonAsync(message);
    }
}

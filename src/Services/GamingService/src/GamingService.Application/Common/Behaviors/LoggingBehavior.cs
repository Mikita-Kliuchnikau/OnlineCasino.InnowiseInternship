using MediatR;
using Microsoft.Extensions.Logging;

namespace GamingService.Application.Common.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(
    ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    where TResponse : notnull
{
    public async Task<TResponse> Handle(
           TRequest request,
           RequestHandlerDelegate<TResponse> next,
           CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling {RequestName} with content: {@Request}", typeof(TRequest).Name, request);

        var response = await next(cancellationToken);

        logger.LogInformation("Handled {RequestName} with response: {@Response}", typeof(TRequest).Name, response);

        return response;
    }
}

using BuildingBlocks.CQRS;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Behaviours;

public class LoggingBehavior<TRequest, TResponse> (ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest: notnull, ICommand<TResponse>
    where TResponse: notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation($"[START] Handle request={typeof(TRequest).Name} " +
            $"- Response={typeof(TResponse).Name} - RequestData = {request}");

        var timer = new Stopwatch();
        timer.Start();
        var response = await next();
        timer.Stop();

        var takenTime = timer.Elapsed;
        var warningTime = 3;
        if(takenTime.Seconds > warningTime)
        {
            logger.LogWarning($"[PERFORMANCE] The reqquest {typeof(TRequest).Name} took {takenTime.Seconds}");
        }

        return response;
    }
}

using MediatR;
using Microsoft.Extensions.Logging;

namespace Shared.Behavior;

public class LoggingBehavior<TRequest, TResponse>(
    ILogger<LoggingBehavior<TRequest, TResponse>> logger) 
    : IPipelineBehavior<TRequest, TResponse>
        where  TRequest : IRequest<TResponse>
        where TResponse : notnull
{
    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken ct)
    {
        logger.LogInformation("[START] Handling request {RequestName} ({@Request})", 
            typeof(TRequest).Name, request);
        
        // other behaviors -> handler -> other behaviors -> response
        var response = await next(ct);
        
        logger.LogInformation("[END] Request {RequestName} handled - response: {@Response}", 
            typeof(TRequest).Name, response);

        return response;
    }
}
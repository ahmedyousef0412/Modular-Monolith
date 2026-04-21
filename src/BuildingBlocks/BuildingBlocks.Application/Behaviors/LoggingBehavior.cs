namespace BuildingBlocks.Application.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest: notnull
{

    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
       _logger.LogInformation("Handling {RequestName} with content {@Request}", typeof(TRequest).Name, request);

        var respnse = await next();

        _logger.LogInformation("Handled {RequestName} with content {@Response}", typeof(TRequest).Name, respnse);

        return respnse;
    }
}

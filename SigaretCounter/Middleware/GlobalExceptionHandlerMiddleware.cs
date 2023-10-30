namespace SigaretCounter.Middleware;

public class GlobalExceptionHandlerMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

    public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var traceId = Guid.NewGuid();
            _logger.LogError($"Error occure while processing the request, TraceId : ${traceId}," +
                $" Message : ${ex.Message}, StackTrace: ${ex.StackTrace}");
        }
    }
}
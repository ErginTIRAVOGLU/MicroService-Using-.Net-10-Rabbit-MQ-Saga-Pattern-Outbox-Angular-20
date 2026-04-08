namespace ApiGateway.Middleware;

public sealed class CorrelationIdMiddleware
{
    private readonly string CorrelationIdHeader = "x-correlation-id";
    private readonly RequestDelegate _next;
    private readonly ILogger<CorrelationIdMiddleware> _logger;
    public CorrelationIdMiddleware(RequestDelegate next, ILogger<CorrelationIdMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Generate if not present
        if(!context.Request.Headers.TryGetValue(CorrelationIdHeader, out var correlationId) )
        {
            correlationId = Guid.NewGuid().ToString();
        }

        context.Response.Headers[CorrelationIdHeader] = correlationId;

        // Log for visibility
        using (_logger.BeginScope("{CorrelationId}", correlationId!))
        {
            _logger.LogInformation("Processing request with Correlation ID: {CorrelationId}", correlationId!);
            await _next(context);
        }
    }
}

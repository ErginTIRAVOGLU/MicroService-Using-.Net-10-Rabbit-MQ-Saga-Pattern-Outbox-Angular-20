using EventBus.Messages.Events;
using MassTransit;

namespace Payment.Consumers;

public sealed class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<OrderCreatedEvent> _logger;

    public OrderCreatedConsumer(IPublishEndpoint publishEndpoint, ILogger<OrderCreatedEvent> logger)
    {
        _publishEndpoint = publishEndpoint;
        _logger = logger;

    }

    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received OrderCreated event for OrderId: {OrderId}", message.Id);

        using (Serilog.Context.LogContext.PushProperty("CorrelationId", message.CorrelationId))
        {
            _logger.LogInformation("Processing payment for OrderId: {OrderId} with CorrelationId: {CorrelationId}", message.Id, message.CorrelationId);
            await Task.Delay(1000); // Simulate payment processing delay
            if (message.TotalPrice > 0)
            {
                var completedEvent = new PaymentCompletedEvent
                {
                    OrderId = message.Id,
                    CorrelationId = message.CorrelationId

                };
                await _publishEndpoint.Publish(completedEvent);
                _logger.LogInformation("Published PaymentCompleted event for OrderId: {OrderId}", message.Id);
            }
            else
            {
                var failedEvent = new PaymentFailedEvent
                {
                    OrderId = message.Id,
                    CorrelationId = message.CorrelationId,


                    Reason = "Invalid total price, Total price was zero or negative."
                };
                await _publishEndpoint.Publish(failedEvent);
                _logger.LogWarning("Payment failed for OrderId: {OrderId} due to invalid total price: {TotalPrice}", message.Id, message.TotalPrice);
            }
        }

    }

}

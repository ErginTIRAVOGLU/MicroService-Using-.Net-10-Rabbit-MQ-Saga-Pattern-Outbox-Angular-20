using EventBus.Messages.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OrderService.Data;

namespace OrderService.Dispatcher;

public sealed class OutboxMessageDispatcher : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<OutboxMessageDispatcher> _logger;

    public OutboxMessageDispatcher(IServiceProvider serviceProvider, ILogger<OutboxMessageDispatcher> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("OutboxMessageDispatcher is starting.");
        while (!cancellationToken.IsCancellationRequested)
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<OrderContext>();
            var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();
            var pendingMessages = await dbContext.OutboxMessages
                                        .Where(m => m.ProcessedOn == null)
                                        .OrderBy(m => m.OccouredOn)
                                        .Take(20)
                                        .ToListAsync(cancellationToken);
            foreach (var message in pendingMessages)
            {
                try
                {
                    var orderCreatedEvent = JsonConvert.DeserializeObject<OrderCreatedEvent>(message.Content);
                    await publishEndpoint.Publish(orderCreatedEvent!, cancellationToken);
                    message.ProcessedOn = DateTime.UtcNow;
                    _logger.LogInformation($"Successfully dispatched outbox message with Id {message.Id}.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error dispatching outbox message with Id {message.Id}. Will retry later.");
                }
            }


            await dbContext.SaveChangesAsync(cancellationToken);
            await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);
        }
    }

     

}

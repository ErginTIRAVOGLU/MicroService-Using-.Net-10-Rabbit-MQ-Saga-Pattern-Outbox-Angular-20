using EventBus.Messages.Events;
using MassTransit;
using OrderService.Entities;
using OrderService.Repositories;

namespace OrderService.Consumer;

public sealed class PaymentFailedConsumer : IConsumer<PaymentFailedEvent>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<PaymentFailedConsumer> _logger;

    public PaymentFailedConsumer(ILogger<PaymentFailedConsumer> logger, IOrderRepository orderRepository)
    {
        _logger = logger;
        _orderRepository = orderRepository;
    }

    public async Task Consume(ConsumeContext<PaymentFailedEvent> context)
    {
        var order = await _orderRepository.GetByIdAsync(context.Message.OrderId);
        if (order is null)
        {
            _logger.LogError("Order with id {OrderId} not found", context.Message.OrderId);
            return;
        }
        order.Status = OrderStatus.Failed;
        await _orderRepository.UpdateAsync(order);
        _logger.LogInformation("Order with id {OrderId} marked as failed", context.Message.OrderId);
    }

}

using EventBus.Messages.Events;
using MassTransit;
using OrderService.Entities;
using OrderService.Repositories;

namespace OrderService.Consumer;

public sealed class PaymentCompletedConsumer : IConsumer<PaymentCompletedEvent>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<PaymentCompletedConsumer> _logger;

    public PaymentCompletedConsumer(ILogger<PaymentCompletedConsumer> logger, IOrderRepository orderRepository)
    {
        _logger = logger;
        _orderRepository = orderRepository;
    }
    
    public async Task Consume(ConsumeContext<PaymentCompletedEvent> context)
    {
        var order = await _orderRepository.GetByIdAsync(context.Message.OrderId);
        if (order is null)
        {
            _logger.LogError($"Order with id {context.Message.OrderId} not found", context.Message.OrderId, context.Message.CorrelationId);
            return;
        }
        order.Status = OrderStatus.Paid;
        await _orderRepository.UpdateAsync(order); 
        _logger.LogInformation($"Order with id {context.Message.OrderId} marked as paid", context.Message.OrderId, context.Message.CorrelationId);
    }

}

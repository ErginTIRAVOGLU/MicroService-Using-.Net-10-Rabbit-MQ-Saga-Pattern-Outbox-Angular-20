using MediatR;
using OrderService.Commands;
using OrderService.Mappers;
using OrderService.Repositories;

namespace OrderService.Handlers;

public sealed class CheckoutOrderHandler : IRequestHandler<CheckoutOrderCommand, int>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<CheckoutOrderHandler> _logger;

    public CheckoutOrderHandler(IOrderRepository orderRepository, ILogger<CheckoutOrderHandler> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;

    }


    public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
    {
        var orderEntity = request.ToEntity();
        try
        {
            var generatedOrder = await _orderRepository.AddAsync(orderEntity);

            var outboxMessage = OrderMapper.ToOutboxMessage(generatedOrder);
            await _orderRepository.AddOutboxMessageAsync(outboxMessage);

            _logger.LogInformation($"Order with Id {generatedOrder.Id} successfully created.");
            return generatedOrder.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while processing order with Id {orderEntity.Id}.");
            throw;
        }

    }

}

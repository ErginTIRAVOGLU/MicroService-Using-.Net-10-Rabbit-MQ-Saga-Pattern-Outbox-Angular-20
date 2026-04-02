using MediatR;
using OrderService.Commands;
using OrderService.Entities;
using OrderService.Exceptions;
using OrderService.Mappers;
using OrderService.Repositories;

namespace OrderService.Handlers;

public sealed class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, Unit>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<UpdateOrderHandler> _logger;
    public UpdateOrderHandler(IOrderRepository orderRepository, ILogger<UpdateOrderHandler> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }
    
    public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderToUpdate = await _orderRepository.GetByIdAsync(request.Id);
        if(orderToUpdate==null)
        {
            throw new OrderNotFoundException(nameof(Order), request.Id);
        }
        orderToUpdate.MapUpdate(request);
        await _orderRepository.UpdateAsync(orderToUpdate);
        _logger.LogInformation($"Order {orderToUpdate.Id} is succesfully updated.");
        return Unit.Value;
    }

}

using MediatR;
using OrderService.DTOs;
using OrderService.Mappers;
using OrderService.Queries;
using OrderService.Repositories;

namespace OrderService.Handlers;

public sealed class GetOrderListHandler : IRequestHandler<GetOrderListQuery, List<OrderDto>>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderListHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }


    public async Task<List<OrderDto>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
    {
        var orders=await _orderRepository.GetOrdersByUserName(request.UserName);
        return orders.Select(o=>o.ToDto()).ToList();
    }

}

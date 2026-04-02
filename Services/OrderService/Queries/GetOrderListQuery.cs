using MediatR;
using OrderService.DTOs;

namespace OrderService.Queries;

public sealed record GetOrderListQuery(string UserName):IRequest<List<OrderDto>>;

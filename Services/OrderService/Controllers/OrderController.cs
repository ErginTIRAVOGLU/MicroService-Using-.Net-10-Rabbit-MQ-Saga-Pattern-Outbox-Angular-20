using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Commands;
using OrderService.DTOs;
using OrderService.Entities;
using OrderService.Mappers;
using OrderService.Queries;

namespace OrderService.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public sealed class OrderController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<OrderController> _logger;

    public OrderController(IMediator mediator, ILogger<OrderController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("{userName}", Name = "GetOrdersByUserName")]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByUserName([FromRoute] string userName)
    {
        var query = new GetOrderListQuery(userName);
        var orders = await _mediator.Send(query);
        _logger.LogInformation($"Orders fetched for user: {userName}");
        return Ok(orders);
    }

    [HttpPost("CheckoutOrder")]
    public async Task<ActionResult<int>> CheckoutOrder([FromBody] CreateOrderDto createOrderDto)
    {
        var command = createOrderDto.ToCommand();
        var result = await _mediator.Send(command);
        _logger.LogInformation($"Order created with Id: {result}");
        return Ok(result);
    }

    [HttpPut("UpdateOrder")]
    public async Task<IActionResult> UpdateOrder([FromBody] OrderDto orderDto)
    {
        var command = orderDto.ToCommand();
        await _mediator.Send(command);
        _logger.LogInformation($"Order updated with Id: {orderDto.Id}");
        return NoContent();
    }

    [HttpDelete("{id}", Name = "DeleteOrder")]
    public async Task<IActionResult> DeleteOrder([FromRoute] int id)
    {
        var command = new DeleteOrderCommand { Id = id };
        await _mediator.Send(command);
        _logger.LogInformation($"Order deleted with Id: {id}");
        return NoContent();
    }

}

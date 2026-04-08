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
        // Extract Correlation id from headers for logging (x-correlation-id)
        var correlationId = Request.Headers["x-correlation-id"].FirstOrDefault() ?? Guid.NewGuid().ToString();

        var command = createOrderDto.ToCommand();

        command.CorrelationId = Guid.Parse(correlationId); // Pass correlation id to command for logging in handlers  
        var result = await _mediator.Send(command);
        _logger.LogInformation($"Order created with Id: {result} and Correlation ID: {correlationId}");
        return Ok(result);
    }

    [HttpPut("UpdateOrder")]
    public async Task<IActionResult> UpdateOrder([FromBody] OrderDto orderDto)
    {
        // Extract Correlation id from headers for logging (x-correlation-id)
        var correlationId = Request.Headers["x-correlation-id"].FirstOrDefault() ?? Guid.NewGuid().ToString();

        var command = orderDto.ToCommand();
        command.CorrelationId =  Guid.Parse(correlationId); // Pass correlation id to command for logging in handlers
        await _mediator.Send(command);
        _logger.LogInformation($"Order updated with Id: {orderDto.Id} and Correlation ID: {correlationId}");
        return NoContent();
    }

    [HttpDelete("{id}", Name = "DeleteOrder")]
    public async Task<IActionResult> DeleteOrder([FromRoute] int id)
    { 
        // Extract Correlation id from headers for logging (x-correlation-id)
        var correlationId = Request.Headers["x-correlation-id"].FirstOrDefault() ?? Guid.NewGuid().ToString();

        var command = new DeleteOrderCommand { Id = id, CorrelationId = correlationId }; // Pass correlation id to command for logging in handlers
        await _mediator.Send(command);
        _logger.LogInformation($"Order deleted with Id: {id} and Correlation ID: {correlationId}");
        return NoContent();
    }

}

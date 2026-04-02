using MediatR;

namespace OrderService.Commands;

public sealed record DeleteOrderCommand : IRequest<Unit>
{
    public int Id { get; set; }
}

using MediatR;

namespace Basket.Commands;

public sealed record DeleteBasketByUserNameCommand (string userName):IRequest<Unit>;

using Basket.DTOs;
using MediatR;

namespace Basket.Commands;

public sealed record CheckoutBasketCommand(BasketCheckoutDto BasketCheckoutDto):IRequest<Unit>;
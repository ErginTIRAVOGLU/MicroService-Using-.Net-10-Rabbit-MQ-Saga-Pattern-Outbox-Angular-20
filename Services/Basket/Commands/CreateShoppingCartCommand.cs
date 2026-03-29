using Basket.DTOs;
using Basket.Responses;
using MediatR;

namespace Basket.Commands;

public sealed record class CreateShoppingCartCommand(string UserName, List<CreateShoppingCartItemDto> Items):IRequest<ShoppingCartResponse>;

using Basket.Entities;
using Basket.Responses;
using MediatR;

namespace Basket.Queries;

public sealed record GetBasketByUserNameQuery(string UserName) : IRequest<ShoppingCartResponse>;
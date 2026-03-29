using Basket.Commands;
using Basket.Mappers;
using Basket.Repositories;
using Basket.Responses;
using MediatR;

namespace Basket.Handlers;

public sealed class CreateShoppingCartHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
{
    private readonly IBasketRepository _basketRepository;

    public CreateShoppingCartHandler(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }


    public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
    {
        var shoppingCartEntity = request.ToEntity();

        var updatedCart = await _basketRepository.UpsertBasket(shoppingCartEntity);

        var respose = (ShoppingCartResponse)updatedCart!;
        return respose;
    }

}

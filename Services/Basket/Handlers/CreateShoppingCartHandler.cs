using Basket.Commands;
using Basket.GrpcService;
using Basket.Mappers;
using Basket.Repositories;
using Basket.Responses;
using MediatR;

namespace Basket.Handlers;

public sealed class CreateShoppingCartHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
{
    private readonly IBasketRepository _basketRepository;
    private readonly DiscountGrpcService _discountGrpcService;

    public CreateShoppingCartHandler(IBasketRepository basketRepository, DiscountGrpcService discountGrpcService)
    {
        _basketRepository = basketRepository;
        _discountGrpcService = discountGrpcService;
    }


    public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
    {
        foreach(var item in request.Items)
        {
            var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
            item.Price -= Convert.ToDecimal(coupon.Amount);
        }
        
        var shoppingCartEntity = request.ToEntity();

        var updatedCart = await _basketRepository.UpsertBasket(shoppingCartEntity);

        var respose = (ShoppingCartResponse)updatedCart!;
        return respose;
    }

}

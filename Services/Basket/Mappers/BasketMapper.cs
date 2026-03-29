using Basket.Commands;
using Basket.Entities;
using Basket.Responses;

namespace Basket.Mappers;

public static class BasketMapper
{
    public static ShoppingCart ToEntity(this CreateShoppingCartCommand request)
    {
        return new ShoppingCart
        {
            UserName = request.UserName,
            Items = request.Items.Select(x => x.ToEntity()).ToList()
        };
    }

    public static ShoppingCart ToEntity(this ShoppingCartResponse response)
    {
        return new ShoppingCart
        {
            UserName = response.UserName,
            Items = response.Items.Select(x => x.ToEntity()).ToList()
        };
    }

    // ✅ Use parentheses, NOT object initializer
    public static ShoppingCartResponse ToResponse(this ShoppingCart shoppingCart)
    {
        return new ShoppingCartResponse(
            shoppingCart.UserName,
            shoppingCart.Items.Select(x => x.ToResponse()).ToList()
        );
    }
}
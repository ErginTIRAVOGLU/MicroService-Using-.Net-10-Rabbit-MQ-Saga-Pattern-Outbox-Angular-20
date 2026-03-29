using Basket.Entities;

namespace Basket.Responses;

public sealed record class ShoppingCartResponse(string UserName, List<ShoppingCartItemResponse> Items)
{
    public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);

    public ShoppingCartResponse(string userName) : this(userName, new List<ShoppingCartItemResponse>())
    {

    }

    public static implicit operator ShoppingCartResponse(ShoppingCart cart)
    {
        return new ShoppingCartResponse(cart.UserName)
        {
            Items = cart.Items.Select(item => new ShoppingCartItemResponse(
                item.ProductId,
                item.ProductName,
                item.Quantity,
                item.Price,
                item.ImageFile
            )).ToList()
        };
    }
}

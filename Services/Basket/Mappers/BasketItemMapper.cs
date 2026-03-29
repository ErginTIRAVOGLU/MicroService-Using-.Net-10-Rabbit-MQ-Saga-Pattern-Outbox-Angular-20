using Basket.DTOs;
using Basket.Entities;
using Basket.Responses;

namespace Basket.Mappers;

public static class BasketItemMapper
{
    public static ShoppingCartItem ToEntity(this CreateShoppingCartItemDto dto)
    {
        return new ShoppingCartItem
        {
            ImageFile = dto.ImageFile,
            Price = dto.Price,
            ProductId = dto.ProductId,
            ProductName = dto.ProductName,
            Quantity = dto.Quantity
        };
    }

    public static ShoppingCartItem ToEntity(this ShoppingCartItemResponse response)
    {
        return new ShoppingCartItem
        {
            ImageFile = response.ImageFile,  // Don't forget this!
            ProductId = response.ProductId,
            ProductName = response.ProductName,
            Price = response.Price,
            Quantity = response.Quantity
        };
    }

    // ✅ ADD THIS METHOD
    public static ShoppingCartItemResponse ToResponse(this ShoppingCartItem item)
    {
        return new ShoppingCartItemResponse(
            item.ProductId,
            item.ProductName,
            item.Quantity,
            item.Price,
            item.ImageFile
        );
    }
}

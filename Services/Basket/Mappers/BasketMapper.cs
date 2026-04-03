using Basket.Commands;
using Basket.DTOs;
using Basket.Entities;
using Basket.Responses;
using EventBus.Messages.Events;

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

    public static BasketCheckoutEvent ToBasketCheckoutEvent(this BasketCheckoutDto dto, ShoppingCart basket)
    {
        return new BasketCheckoutEvent
        {
            UserName = dto.UserName,
            TotalPrice = basket.Items.Sum(item => item.Price * item.Quantity),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            EmailAddres = dto.EmailAddress,
            AddressLine = dto.AddressLine,
            CardName = dto.CardName,
            CardNumber = dto.CardNumber,
            Country = dto.Country,
            Cvv = dto.Cvv,
            State = dto.State,
            PaymentMethod = dto.PaymentMethod,
            Expiration = dto.Expiration,
            ZipCode=dto.ZipCode
        };
    }

    /*
        public static ShoppingCart ToEntity(this ShoppingCartResponse response)
        {
            return new ShoppingCart(response.UserName)
            {
                Items= response.Items.Select(item => new ShoppingCartItem
                {
                    ProductId = item.ProductId,
                    ProductName=item.ProductName,
                    Price=item.Price,
                    Quantity=item.Quantity
                }).ToList()
            };
        }
    */
}
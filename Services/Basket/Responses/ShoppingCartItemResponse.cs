namespace Basket.Responses;

public sealed record class ShoppingCartItemResponse(
    string ProductId,
    string ProductName,
    int Quantity,
    decimal Price,
    string ImageFile
);
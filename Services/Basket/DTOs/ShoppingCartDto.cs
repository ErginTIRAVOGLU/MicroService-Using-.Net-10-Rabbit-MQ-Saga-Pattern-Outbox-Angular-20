namespace Basket.DTOs;

public sealed class ShoppingCartDto
{
    public string UserName { get; set; } = string.Empty;
    public List<ShoppingCartItemDto> Items { get; set; } = new List<ShoppingCartItemDto>();
}

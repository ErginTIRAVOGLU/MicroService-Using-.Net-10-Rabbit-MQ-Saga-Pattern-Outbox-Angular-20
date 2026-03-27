using Catalog.Entities;

namespace Catalog.Responses.Products;

public sealed class ProductResponse
{
    public string? Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageFile { get; set; } = string.Empty;
    public ProductBrand Brand { get; set; } = new ProductBrand();
    public ProductType Type { get; set; } = new ProductType();
    public decimal Price { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset? UpdatedDate { get; set; }

    public static implicit operator ProductResponse(Product product)
    {
        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Summary=product.Summary,
            Description=product.Description,
            ImageFile=product.ImageFile,
            Brand=product.Brand,
            Type=product.Type,
            Price=product.Price,
            CreatedDate=product.CreatedDate,
            UpdatedDate=product.UpdatedDate            
        };
    }
}

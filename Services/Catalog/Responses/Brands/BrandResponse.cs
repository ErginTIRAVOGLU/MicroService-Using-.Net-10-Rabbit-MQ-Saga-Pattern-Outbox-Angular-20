using Catalog.Entities;

namespace Catalog.Responses.Brands;

public sealed class BrandResponse
{
    public string? Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public static implicit operator BrandResponse(ProductBrand brand)
    {
        return new BrandResponse
        {
            Id = brand.Id,
            Name = brand.Name 
        };
    }
}

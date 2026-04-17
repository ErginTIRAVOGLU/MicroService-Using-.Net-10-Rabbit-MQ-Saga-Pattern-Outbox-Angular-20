using Catalog.DTOs;
using Catalog.Responses.Products;

namespace Catalog.Mappers;

public static class ProductMapper
{
    public static ProductDto? toDto(this ProductResponse product)
    {
        if (product == null)
        {
            return null;
        }

        return new ProductDto(
            product.Id!,
            product.Name,
            product.Summary,
            product.Description,
            product.ImageFile,
            product.Price,
            new BrandDto(product.Brand.Id!,product.Brand.Name),
            new TypeDto(product.Type.Id!,product.Type.Name),
            product.CreatedDate
        );
    }
}

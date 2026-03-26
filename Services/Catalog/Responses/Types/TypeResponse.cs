using Catalog.Entities;

namespace Catalog.Responses.Types;

public sealed class TypeResponse
{
    public string? Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public static implicit operator TypeResponse(ProductType brand)
    {
        return new TypeResponse
        {
            Id = brand.Id,
            Name = brand.Name
        };
    }
}

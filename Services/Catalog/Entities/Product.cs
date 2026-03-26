using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Entities;

public sealed class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageFile { get; set; } = string.Empty;
    public ProductBrand Brand { get; set; } = new ProductBrand();
    public ProductType Type { get; set; } = new ProductType();
    
    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Price { get; set; }
   

}

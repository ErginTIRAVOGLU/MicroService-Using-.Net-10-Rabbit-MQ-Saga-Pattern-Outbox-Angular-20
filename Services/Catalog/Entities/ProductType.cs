using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Entities;

public sealed class ProductType :BaseEntity
{
    [BsonElement("Name")]
    public string Name { get; set; }=string.Empty;
}

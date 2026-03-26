using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Entities;

public class BaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedDate { get; set; } = null;

}

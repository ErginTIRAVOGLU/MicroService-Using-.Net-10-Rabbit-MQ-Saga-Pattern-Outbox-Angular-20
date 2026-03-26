using Catalog.Entities;
using MongoDB.Driver;

namespace Catalog.Repositories;

public sealed class TypeRepository : ITypeRepository
{
    private readonly IMongoCollection<ProductType> _types;

    public TypeRepository(IMongoClient client, IConfiguration config)
    {
        var db = client.GetDatabase(config["DatabaseSettings:DatabaseName"]);
        _types = db.GetCollection<ProductType>(config["DatabaseSettings:TypeCollectionName"]);
    }

    public async Task<IEnumerable<ProductType>> GetAllTypesAsync()
    {
        return await _types
            .Find(_ => true)
            .ToListAsync();
    }

    public async Task<ProductType?> GetByIdAsync(string id)
    {
        return await _types
            .Find(t => t.Id == id)
            .FirstOrDefaultAsync();
    }

     public async Task AddManyTypesAsync(IEnumerable<ProductType> types)
    {
        if (types == null || !types.Any())
            return;
            
        await _types.InsertManyAsync(types);
    }

    public async Task<ProductType> CreateTypeAsync(ProductType type)
    {
        await _types.InsertOneAsync(type);
        return type;
    }

  
}
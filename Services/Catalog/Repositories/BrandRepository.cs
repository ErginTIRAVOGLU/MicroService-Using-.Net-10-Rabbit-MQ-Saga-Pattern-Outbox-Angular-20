using Catalog.Entities;
using MongoDB.Driver;

namespace Catalog.Repositories;

public sealed class BrandRepository : IBrandRepository
{
    private readonly IMongoCollection<ProductBrand> _brands;

    public BrandRepository(IMongoClient client, IConfiguration config)
    {
        var db = client.GetDatabase(config["DatabaseSettings:DatabaseName"]);
        _brands = db.GetCollection<ProductBrand>(config["DatabaseSettings:BrandCollectionName"]);
    }

    public async Task AddManyBrandsAsync(IEnumerable<ProductBrand> brands)
    {
        if (brands == null || !brands.Any())
            return;
            
        await _brands.InsertManyAsync(brands);
    }

    public async Task<ProductBrand> CreateBrandAsync(ProductBrand brand)
    {
        await _brands.InsertOneAsync(brand);
        return brand;
    }

    public async Task<IEnumerable<ProductBrand>> GetAllBrandsAsync()
    {
        return await _brands
            .Find(_ => true)
            .ToListAsync();
    }

    public async Task<ProductBrand?> GetByIdAsync(string id)
    {
        return await _brands
            .Find(b => b.Id == id)
            .FirstOrDefaultAsync();
    }
}
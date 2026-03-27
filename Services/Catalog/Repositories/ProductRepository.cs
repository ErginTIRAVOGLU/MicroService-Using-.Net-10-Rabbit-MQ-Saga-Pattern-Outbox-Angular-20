using Catalog.Entities;
using Catalog.Specifications;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Repositories;

public sealed class ProductRepository : IProductRepository
{
    private readonly IMongoCollection<Product> _products;

    public ProductRepository(IMongoClient client, IConfiguration config)
    {
        var db = client.GetDatabase(config["DatabaseSettings:DatabaseName"]);
        _products = db.GetCollection<Product>(config["DatabaseSettings:ProductCollectionName"]);
    }

    public async Task AddManyProductsAsync(IEnumerable<Product> products)
    {
        if (products == null || !products.Any())
            return;

        await _products.InsertManyAsync(products);
    }


    public async Task<Product> CreateProductAsync(Product product)
    {
        await _products.InsertOneAsync(product);
        return product;
    }

    public async Task<bool> DeleteProductAsync(string id)
    {
        var deletedProduct = await _products.DeleteOneAsync(p => p.Id == id);
        return deletedProduct.IsAcknowledged && deletedProduct.DeletedCount > 0;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _products.Find(p => true).ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(string id)
    {
        return await _products.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Pagination<Product>> GetProductsAsync(CatalogSpecParams catalogSpecParams)
    {
        var builder = Builders<Product>.Filter;
        var filter = builder.Empty;

        if (!string.IsNullOrEmpty(catalogSpecParams.Search))
        {
            filter &= builder.Where(p => p.Name.ToLower().Contains(catalogSpecParams.Search.ToLower()));
        }

        if (!string.IsNullOrEmpty(catalogSpecParams.BrandId))
        {
            filter &= builder.Where(p => p.Brand.Id == catalogSpecParams.BrandId);
        }

        if (!string.IsNullOrEmpty(catalogSpecParams.BrandName))
        {
            filter &= builder.Where(p => p.Brand.Name == catalogSpecParams.BrandName);
        }

        if (!string.IsNullOrEmpty(catalogSpecParams.TypeId))
        {
            filter &= builder.Where(p => p.Type.Id == catalogSpecParams.TypeId);
        }

        if (!string.IsNullOrEmpty(catalogSpecParams.TypeName))
        {
            filter &= builder.Where(p => p.Type.Name == catalogSpecParams.TypeName);
        }

        var totalItems = await _products.CountDocumentsAsync(filter);
        var data = await ApplyDataFilters(catalogSpecParams, filter);

        return new Pagination<Product>(
            catalogSpecParams.PageIndex,
            catalogSpecParams.PageSize,
            (int)totalItems,
            data);
    }

    public async Task<IEnumerable<Product>> GetProductsByBrandIdAsync(string brandId)
    {
        return await _products.Find(p => p.Brand.Id == brandId).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByBrandNameAsync(string brandName)
    {
        return await _products.Find(p => p.Brand.Name == brandName).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
    {
        var filter = Builders<Product>.Filter.Regex(p => p.Name, new BsonRegularExpression($".*{name}.*", "i"));
        return await _products.Find(filter).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByTypeIdAsync(string typeId)
    {
        return await _products.Find(p => p.Type.Id == typeId).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByTypeNameAsync(string typeName)
    {
        return await _products.Find(p => p.Type.Name == typeName).ToListAsync();
    }

    public async Task<bool> UpdateProductAsync(Product product)
    {
        var updatedProduct = await _products.ReplaceOneAsync(p => p.Id == product.Id, product);
        return updatedProduct.IsAcknowledged && updatedProduct.ModifiedCount > 0;
    }

    private async Task<IReadOnlyCollection<Product>> ApplyDataFilters(CatalogSpecParams catalogSpecParams, FilterDefinition<Product> filter)
    {
        var sortDefn = catalogSpecParams.Sort switch
        {
            "priceAsc" => Builders<Product>.Sort.Ascending(p => p.Price),
            "priceDesc" => Builders<Product>.Sort.Descending(p => p.Price),
            "nameDesc" => Builders<Product>.Sort.Descending(p => p.Name),
            _ => Builders<Product>.Sort.Ascending(p => p.Name)
        };

        return await _products
            .Find(filter)
            .Sort(sortDefn)
            .Skip((catalogSpecParams.PageIndex - 1) * catalogSpecParams.PageSize)
            .Limit(catalogSpecParams.PageSize)
            .ToListAsync();
    }
}
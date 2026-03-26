using Catalog.Entities;
using Catalog.Specifications;

namespace Catalog.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product?> GetByIdAsync(string id);
    Task<Product> CreateProductAsync(Product product);
    Task<bool> UpdateProductAsync(Product product);
    Task<bool> DeleteProductAsync(string id);
    Task<Pagination<Product>> GetProductsAsync(CatalogSpecParams catalogSpecParams);
    Task<IEnumerable<Product>> GetProductsByBrandIdAsync(string brandId);
    Task<IEnumerable<Product>> GetProductsByBrandNameAsync(string brandName);
    Task<IEnumerable<Product>> GetProductsByNameAsync(string name);
    Task<IEnumerable<Product>> GetProductsByTypeIdAsync(string typeId);
    Task<IEnumerable<Product>> GetProductsByTypeNameAsync(string typeName);
    Task AddManyProductsAsync(IEnumerable<Product> products);
}
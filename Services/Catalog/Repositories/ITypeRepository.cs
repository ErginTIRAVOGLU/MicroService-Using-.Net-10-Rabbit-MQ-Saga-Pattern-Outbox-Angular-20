using Catalog.Entities;

namespace Catalog.Repositories;

public interface ITypeRepository
{
    Task<IEnumerable<ProductType>> GetAllTypesAsync();
    Task<ProductType?> GetByIdAsync(string id);
    Task AddManyTypesAsync(IEnumerable<ProductType> types);
    Task<ProductType> CreateTypeAsync(ProductType type);
}

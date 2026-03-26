using Catalog.Entities;

namespace Catalog.Repositories;

public interface IBrandRepository
{
    Task<IEnumerable<ProductBrand>> GetAllBrandsAsync();
    Task<ProductBrand?> GetByIdAsync(string id);
    Task AddManyBrandsAsync(IEnumerable<ProductBrand> brands);
    Task<ProductBrand> CreateBrandAsync(ProductBrand brand);
}
using System.Text.Json;
using Catalog.Entities;
using Catalog.Repositories;

namespace Catalog.Data;

public sealed class DatabaseSeeder
{
    private readonly IProductRepository _productRepository;
    private readonly IBrandRepository _brandRepository;
    private readonly ITypeRepository _typeRepository;

    public DatabaseSeeder(
        IProductRepository productRepository,
        IBrandRepository brandRepository,
        ITypeRepository typeRepository)
    {
        _productRepository = productRepository;
        _brandRepository = brandRepository;
        _typeRepository = typeRepository;
    }

    public async Task SeedAsync(IConfiguration configuration)
    {
        var seedBasePath = Path.Combine("Data", "SeedData");



        // Brand seed
        var brands = await _brandRepository.GetAllBrandsAsync();
        List<ProductBrand> brandList;

        if (!brands.Any())
        {
            var brandData = await File.ReadAllTextAsync(Path.Combine(seedBasePath, "brands.json"));
            brandList = JsonSerializer.Deserialize<List<ProductBrand>>(brandData) ?? new List<ProductBrand>();
            foreach (var brand in brandList)
            {
                //brand.Id = null;
                if (brand.CreatedDate == default)
                {
                    brand.CreatedDate = DateTime.UtcNow;
                }
            }
            await _brandRepository.AddManyBrandsAsync(brandList);
        }
        else
        {
            brandList = brands.ToList();
        }


        // Type seed
        var types = await _typeRepository.GetAllTypesAsync();
        List<ProductType> typeList;

        if (!types.Any())
        {
            var typeData = await File.ReadAllTextAsync(Path.Combine(seedBasePath, "types.json"));
            typeList = JsonSerializer.Deserialize<List<ProductType>>(typeData) ?? new List<ProductType>();
            foreach (var type in typeList)
            {
                //type.Id = null;
                if (type.CreatedDate == default)
                {
                    type.CreatedDate = DateTime.UtcNow;
                }
            }
            await _typeRepository.AddManyTypesAsync(typeList);
        }
        else
        {
            typeList = types.ToList();
        }

        // Product seed
        var products = await _productRepository.GetAllProductsAsync();
        List<Product> productList;

        if (!products.Any())
        {
            var productData = await File.ReadAllTextAsync(Path.Combine(seedBasePath, "products.json"));
            productList = JsonSerializer.Deserialize<List<Product>>(productData) ?? new List<Product>();
            foreach (var product in productList)
            {
                product.Id = null;
                if (product.CreatedDate == default)
                {
                    product.CreatedDate = DateTime.UtcNow;
                }
            }
            await _productRepository.AddManyProductsAsync(productList);
        }
        else
        {
            productList = products.ToList();
        }
    }
}
using Catalog.Commands.Products;
using Catalog.Entities;
using Catalog.Repositories;
using Catalog.Responses.Products;
using MediatR;

namespace Catalog.Handlers.Products;

public sealed class CreateProductHandler : IRequestHandler<CreateProductCommand, ProductResponse>
{
    private readonly IProductRepository _productRepository;
    private readonly IBrandRepository _brandRepository;
    private readonly ITypeRepository _typeRepository;

    public CreateProductHandler(IProductRepository productRepository, IBrandRepository brandRepository, ITypeRepository typeRepository)
    {
        _productRepository = productRepository;
        _brandRepository = brandRepository;
        _typeRepository = typeRepository;
    }


    public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var brand = await _brandRepository.GetByIdAsync(request.BrandId);
        var type = await _typeRepository.GetByIdAsync(request.TypeId);

        if (brand == null || type == null)
        {
            throw new ApplicationException("Imvalid Brand or Type specified");
        }

        var productEntity = new Product
        {
            Description = request.Description,
            ImageFile = request.ImageFile,
            Name = request.Name,
            Price = request.Price,
            Summary = request.Summary,
            Brand = brand,
            Type = type
        };

        var status = await _productRepository.CreateProductAsync(productEntity);
        return (ProductResponse)status;
    }

}

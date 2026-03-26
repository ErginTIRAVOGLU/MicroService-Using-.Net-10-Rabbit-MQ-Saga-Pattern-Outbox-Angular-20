
using Catalog.Commands.Products;
using Catalog.Entities;
using Catalog.Repositories;
using MediatR;

namespace Catalog.Handlers.Products;

public sealed class UpdateProductHandler : IRequestHandler<UpdateProductCommand, bool>
{
    private readonly IProductRepository _productRepository;
    private readonly IBrandRepository _brandRepository;
    private readonly ITypeRepository _typeRepository;
    public UpdateProductHandler(IProductRepository productRepository, IBrandRepository brandRepository, ITypeRepository typeRepository)
    {
        _productRepository = productRepository;
        _brandRepository = brandRepository;
        _typeRepository = typeRepository;
    }

    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var existing = await _productRepository.GetByIdAsync(request.Id);
        if (existing == null)
        {
            throw new KeyNotFoundException($"Product with Id {request.Id} not found.");
        }

        var brand = await _brandRepository.GetByIdAsync(request.BrandId);
        var type = await _typeRepository.GetByIdAsync(request.TypeId);

        if (brand == null || type == null)
        {
            throw new ApplicationException("Imvalid Brand or Type specified");
        }

        var updatedProduct = new Product
        {
            Brand = brand,
            Type = type,
            Description = request.Description,
            Id = request.Id,
            ImageFile = request.ImageFile,
            Name = request.Name,
            Price = request.Price,
            Summary = request.Summary,
            CreatedDate = request.CreatedDate,
            UpdatedDate = DateTimeOffset.UtcNow
        };

        return await _productRepository.UpdateProductAsync(updatedProduct);
    }
}
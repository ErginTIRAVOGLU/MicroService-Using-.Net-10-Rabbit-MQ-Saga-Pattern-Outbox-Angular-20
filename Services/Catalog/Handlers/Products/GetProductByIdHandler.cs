using Catalog.Queries.Products;
using Catalog.Repositories;
using Catalog.Responses.Products;
using MediatR;

namespace Catalog.Handlers.Products;

public sealed class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductResponse>
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }


    public async Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);
        
        var productResponse = (ProductResponse)product!;
        return productResponse;

    }

}

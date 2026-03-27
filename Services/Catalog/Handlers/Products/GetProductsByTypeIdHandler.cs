using Catalog.Queries.Products;
using Catalog.Repositories;
using Catalog.Responses.Products;
using MediatR;

namespace Catalog.Handlers.Products;

public sealed class GetProductsByTypeIdHandler : IRequestHandler<GetProductsByTypeQuery, IList<ProductResponse>>
{
    private readonly IProductRepository _productRepository;

    public GetProductsByTypeIdHandler(IProductRepository productRepository)
    {
        _productRepository=productRepository;
    }
    public async Task<IList<ProductResponse>> Handle(GetProductsByTypeQuery request, CancellationToken cancellationToken)
    {
        var productList=await _productRepository.GetProductsByTypeIdAsync(request.Id);
        return productList.Select(p => (ProductResponse)p).ToList();
    }

}

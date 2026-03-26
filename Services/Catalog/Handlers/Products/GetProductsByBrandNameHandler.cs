using Catalog.Queries.Products;
using Catalog.Repositories;
using Catalog.Responses.Products;
using MediatR;

namespace Catalog.Handlers.Products;

public sealed class GetProductsByBrandNameHandler : IRequestHandler<GetProductsByBrandQuery, IList<ProductResponse>>
{
    private readonly IProductRepository _productRepository;

    public GetProductsByBrandNameHandler(IProductRepository productRepository)
    {
        _productRepository=productRepository;
    }
    public async Task<IList<ProductResponse>> Handle(GetProductsByBrandQuery request, CancellationToken cancellationToken)
    {
        var productList=await _productRepository.GetProductsByBrandNameAsync(request.BrandName!);
        return productList.Select(p => (ProductResponse)p).ToList();
    }

}

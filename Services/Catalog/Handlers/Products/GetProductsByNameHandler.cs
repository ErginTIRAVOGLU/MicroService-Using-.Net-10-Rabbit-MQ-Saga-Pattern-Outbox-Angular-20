using Catalog.Queries.Products;
using Catalog.Repositories;
using Catalog.Responses.Products;
using MediatR;

namespace Catalog.Handlers.Products;

public sealed class GetProductsByNameHandler : IRequestHandler<GetProductsByNameQuery, IList<ProductResponse>>
{
    private readonly IProductRepository _productRepository;
    public GetProductsByNameHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IList<ProductResponse>> Handle(GetProductsByNameQuery request, CancellationToken cancellationToken)
    {
        var productList= await _productRepository.GetProductsByNameAsync(request.Name);
        return productList.Select(p=> (ProductResponse)p).ToList();
    }

}

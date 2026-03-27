using Catalog.Queries.Products;
using Catalog.Repositories;
using Catalog.Responses.Products;
using Catalog.Specifications;
using MediatR;

namespace Catalog.Handlers.Products;

public sealed class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, Pagination<ProductResponse>>
{
    private readonly IProductRepository _productRepository;
    public GetAllProductsHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Pagination<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var productList = await _productRepository.GetProductsAsync(request.CatalogSpecParams);
        var productResponseList = productList.Data.Select(p => (ProductResponse)p).ToList();
        return new Pagination<ProductResponse>(
            productList.PageIndex,   // Sayfa numarası
            productList.PageSize,    // Sayfa boyutu
            productList.Count,       // Toplam kayıt sayısı
            productResponseList      // Dönüştürülmüş data
        );
    }

}

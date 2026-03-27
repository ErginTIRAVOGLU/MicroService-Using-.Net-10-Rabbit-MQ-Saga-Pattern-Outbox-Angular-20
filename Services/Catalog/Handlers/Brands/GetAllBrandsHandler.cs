using Catalog.Queries.Brands;
using Catalog.Repositories;
using Catalog.Responses.Brands;
using MediatR;

namespace Catalog.Handlers.Brands;

public sealed class GetAllBrandsHandler : IRequestHandler<GetAllBrandsQuery, IList<BrandResponse>>
{
    private readonly IBrandRepository _brandRepository;

    public GetAllBrandsHandler(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task<IList<BrandResponse>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
    {
        var brandList = await _brandRepository.GetAllBrandsAsync();
        return brandList.Select(b=> (BrandResponse)b).ToList();
    }

}

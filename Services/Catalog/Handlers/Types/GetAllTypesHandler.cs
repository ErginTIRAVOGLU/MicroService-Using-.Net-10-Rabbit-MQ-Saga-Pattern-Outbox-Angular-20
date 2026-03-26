using Catalog.Queries;
using Catalog.Queries.Types;
using Catalog.Repositories;
using Catalog.Responses;
using Catalog.Responses.Types;
using MediatR;

namespace Catalog.Handlers.Types;

public sealed class GetAllTypesHandler : IRequestHandler<GetAllTypesQuery, IList<TypeResponse>>
{
    private readonly ITypeRepository _typeRepository;

    public GetAllTypesHandler(ITypeRepository typeRepository)
    {
        _typeRepository = typeRepository;
    }


    public async Task<IList<TypeResponse>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
    {
        var typeList = await _typeRepository.GetAllTypesAsync();
        return typeList.Select(t=> (TypeResponse)t).ToList();
    }

}

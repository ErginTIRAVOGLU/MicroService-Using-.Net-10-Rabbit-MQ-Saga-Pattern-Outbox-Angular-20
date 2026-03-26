using Catalog.Responses;
using Catalog.Responses.Types;
using MediatR;

namespace Catalog.Queries.Types;

public sealed record GetAllTypesQuery:IRequest<IList<TypeResponse>>;
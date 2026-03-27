using Catalog.Responses.Brands;
using MediatR;

namespace Catalog.Queries.Brands;

public sealed record GetAllBrandsQuery:IRequest<IList<BrandResponse>>;

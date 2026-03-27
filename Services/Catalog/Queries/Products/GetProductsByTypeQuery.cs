using Catalog.Responses.Products;
using MediatR;

namespace Catalog.Queries.Products;

public sealed record GetProductsByTypeQuery(string Id):IRequest<IList<ProductResponse>>;
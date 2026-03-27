using Catalog.Responses.Products;
using MediatR;

namespace Catalog.Queries.Products;

public sealed record GetProductsByNameQuery(string Name):IRequest<IList<ProductResponse>>;
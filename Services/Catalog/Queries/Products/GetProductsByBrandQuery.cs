using Catalog.Responses;
using Catalog.Responses.Products;
using MediatR;

namespace Catalog.Queries.Products;

public sealed record GetProductsByBrandQuery(string? Id, string? BrandName):IRequest<IList<ProductResponse>>;
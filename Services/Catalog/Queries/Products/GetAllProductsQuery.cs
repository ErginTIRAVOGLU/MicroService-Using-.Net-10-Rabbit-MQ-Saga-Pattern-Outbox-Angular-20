using Catalog.Entities;
using Catalog.Responses;
using Catalog.Responses.Products;
using Catalog.Specifications;
using MediatR;

namespace Catalog.Queries.Products;

public sealed record GetAllProductsQuery(CatalogSpecParams CatalogSpecParams): IRequest<Pagination<ProductResponse>>;
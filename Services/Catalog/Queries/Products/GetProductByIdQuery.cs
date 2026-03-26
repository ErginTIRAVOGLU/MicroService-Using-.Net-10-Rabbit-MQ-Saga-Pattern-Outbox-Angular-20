using Catalog.Responses.Products;
using MediatR;

namespace Catalog.Queries.Products;

public sealed record GetProductByIdQuery(string Id) : IRequest<ProductResponse>;

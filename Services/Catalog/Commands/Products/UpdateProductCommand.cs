using MediatR;

namespace Catalog.Commands.Products;

public sealed record UpdateProductCommand(
    string Id,
    string Name,
    string Summary,
    string Description,
    string ImageFile,
    string BrandId,
    string TypeId,
    decimal Price
) : IRequest<bool>;

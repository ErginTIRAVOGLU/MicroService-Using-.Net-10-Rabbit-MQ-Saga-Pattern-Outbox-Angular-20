using MediatR;

namespace Catalog.Commands.Products;

public sealed record DeleteProductCommand(string Id) : IRequest<bool>;

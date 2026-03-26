using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Catalog.Responses.Products;
using MediatR;

namespace Catalog.Commands.Products;

public sealed class UpdateProductCommand : IRequest<bool>
{
    public string Id { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Summary { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string ImageFile { get; init; } = string.Empty;
    public string BrandId { get; init; } = string.Empty;
    public string TypeId { get; init; } = string.Empty;

    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; init; }
    public DateTimeOffset CreatedDate { get; init; }
    public DateTimeOffset? UpdatedDate { get; init; } = null;
}

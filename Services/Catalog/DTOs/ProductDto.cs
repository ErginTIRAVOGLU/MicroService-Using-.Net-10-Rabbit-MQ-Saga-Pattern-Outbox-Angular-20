using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.DTOs;

public  sealed record ProductDto(
    string Id,
    string Name,
    string Summary,
    string Description,
    string ImageFile,
    BrandDto Brand,
    TypeDto Type,
    DateTimeOffset CreatedDate
);

public sealed class CreateProductDto
{
    [Required]
    public string Name { get; init; } = default!;
    [Required]
    public string Summary { get; init; } = default!;
    [Required]
    public string Description { get; init; } = default!;
    [Required]
    public string ImageFile { get; init; } = default!;
    [Required]
    public string BrandId { get; init; } = default!;
    [Required]
    public string TypeId { get; init; } = default!;
    [Required]
    [Range(0.01,double.MaxValue,ErrorMessage ="Price must be greater than 0")]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; init; }
}

public sealed class UpdateProductDto
{
    [Required]
    public string Name { get; init; }  = default!;
    [Required]
    public string Summary { get; init; } = default!;
    [Required]
    public string Description { get; init; } = default!;
    [Required]
    public string ImageFile { get; init; } = default!;
    [Required]
    public string BrandId { get; init; } = default!;
    [Required]
    public string TypeId { get; init; } = default!;
    [Required]
    [Range(0.01,double.MaxValue,ErrorMessage ="Price must be greater than 0")]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; init; }
}



 
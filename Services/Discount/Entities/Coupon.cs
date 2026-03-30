using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Discount.Entities;

public sealed class Coupon
{
    public int Id { get; set; }
    public string ProductName { get; set; }=string.Empty;
    public string Description { get; set; }=string.Empty;
    [Column(TypeName = "numeric(8,2)")]
    public decimal Amount { get; set; }

    
}

namespace Discount.DTOs;

public sealed record CouponDto(int Id, string ProductName, string Description, decimal Amount);
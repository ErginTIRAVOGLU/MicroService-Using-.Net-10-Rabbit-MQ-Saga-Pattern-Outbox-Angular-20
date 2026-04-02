namespace OrderService.DTOs;

public sealed record OrderDto(
    int Id,
    string? UserName,
    decimal? TotalPrice,
    string? FirstName,
    string? LastName,
    string? EmailAddress,
    string? AddressLine,
    string? Country,
    string? State,
    string? ZipCode,
    string? CardName,
    string? CardNumber,
    string? Expiration,
    string? Cvv,
    int? PaymentMethod
);


public sealed record CreateOrderDto(
    string? UserName,
    decimal? TotalPrice,
    string? FirstName,
    string? LastName,
    string? EmailAddress,
    string? AddressLine,
    string? Country,
    string? State,
    string? ZipCode,
    string? CardName,
    string? CardNumber,
    string? Expiration,
    string? Cvv,
    int? PaymentMethod
);
using Discount.DTOs;
using MediatR;

namespace Discount.Commands;

public sealed record CreateDiscountCommand(string ProductName, string Description, decimal Amount):IRequest<CouponDto>;
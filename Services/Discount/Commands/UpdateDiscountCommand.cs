using Discount.DTOs;
using MediatR;

namespace Discount.Commands;

public sealed record UpdateDiscountCommand(int Id, string ProductName, string Description, decimal Amount):IRequest<CouponDto>;
 

using Discount.DTOs;
using MediatR;

namespace Discount.Queries;

public sealed record GetDiscountQuery(string ProductName):IRequest<CouponDto>
{
    
}
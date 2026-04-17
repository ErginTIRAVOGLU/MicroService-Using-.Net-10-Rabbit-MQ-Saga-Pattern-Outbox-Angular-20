using Discount.DTOs;
using Discount.Extensions;
using Discount.Mappers;
using Discount.Queries;
using Discount.Repositories;
using Grpc.Core;
using MediatR;

namespace Discount.Handlers;

public sealed class GetDiscountHandler : IRequestHandler<GetDiscountQuery, CouponDto>
{
    private readonly IDiscountRepository _discountRepository;

    public GetDiscountHandler(IDiscountRepository discountRepository)
    {
        _discountRepository = discountRepository;
    }


    public async Task<CouponDto> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.ProductName))
        {
            var validationErrors = new Dictionary<string, string>
            {
                {"ProductName","Product name must not be empty"}
            };
        }

        var coupon = await _discountRepository.GetDiscount(request.ProductName); // productName -> ProductName
        if (coupon == null)
        {
            return new CouponDto(0, request.ProductName, "No discount", 0);
            // throw new RpcException(new Status(StatusCode.NotFound, $"Discount for the Product Name = {request.ProductName} not found"));
        }

        return coupon.ToDto();
    }

}

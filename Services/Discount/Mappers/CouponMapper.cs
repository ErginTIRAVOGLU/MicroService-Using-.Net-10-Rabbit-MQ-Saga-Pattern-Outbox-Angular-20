using Discount.Commands;
using Discount.DTOs;
using Discount.Entities;
using Discount.Grpc.Protos;
using Discount.Handlers;

namespace Discount.Mappers;

public static class CouponMapper
{
    public static CouponDto ToDto(this Coupon coupon)
    {
        return new CouponDto(
            coupon.Id,
            coupon.ProductName,
            coupon.Description,
            coupon.Amount
        );
    }

    public static Coupon ToEntity(this CreateDiscountCommand command)
    {
        return new Coupon
        {
            Amount = command.Amount,
            Description = command.Description,
            ProductName = command.ProductName
        };
    }
    public static Coupon ToEntity(this UpdateDiscountCommand command)
    {
        return new Coupon
        {
            Id = command.Id,
            Amount = command.Amount,
            Description = command.Description,
            ProductName = command.ProductName
        };
    }

    public static CouponModel ToModel(this CouponDto model)
    {
        return new CouponModel
        {
            Amount=model.Amount.ToString(),
            Description=model.Description,
            Id=model.Id,
            ProductName=model.ProductName
        };
        
    }

    public static CreateDiscountCommand ToCreateCommand(this CouponModel model)
    {
        return new CreateDiscountCommand(
            model.ProductName,
            model.Description,
            Convert.ToDecimal(model.Amount)
        );
    }

    public static UpdateDiscountCommand ToUpdateCommand(this CouponModel model)
    {
        return new UpdateDiscountCommand(
            model.Id,
            model.ProductName,
            model.Description,
           Convert.ToDecimal(model.Amount)
        );
    }
}

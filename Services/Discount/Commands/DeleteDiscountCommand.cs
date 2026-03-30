using MediatR;

namespace Discount.Commands;

public sealed record DeleteDiscountCommand(string ProductName):IRequest<bool>;
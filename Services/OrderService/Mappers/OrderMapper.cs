using EventBus.Messages.Events;
using OrderService.Commands;
using OrderService.DTOs;
using OrderService.Entities;

namespace OrderService.Mappers;

public static class OrderMapper
{
    public static OrderDto ToDto(this Order order) =>
        new OrderDto(order.Id, order.UserName!, order.TotalPrice!,
            order.FirstName!, order.LastName!, order.EmailAddress!,
            order.AddressLine!, order.Country!, order.State!,
            order.ZipCode!, order.CardName!, order.CardNumber!,
            order.Expiration!, order.Cvv!, order.PaymentMethod!);
    public static Order ToEntity(this CheckoutOrderCommand orderCommand)
    {
        return new Order
        {
            AddressLine = orderCommand.AddressLine,
            CardName = orderCommand.CardName,
            CardNumber = orderCommand.CardNumber,
            Country = orderCommand.Country,
            Cvv = orderCommand.Cvv,
            EmailAddress = orderCommand.EmailAddress,
            Expiration = orderCommand.Expiration,
            FirstName = orderCommand.FirstName,
            LastName = orderCommand.LastName,
            PaymentMethod = orderCommand.PaymentMethod,
            State = orderCommand.State,
            TotalPrice = orderCommand.TotalPrice,
            UserName = orderCommand.UserName,
            ZipCode = orderCommand.ZipCode
        };
    }
    public static CheckoutOrderCommand ToCommand(this CreateOrderDto createOrderDto)
    {
        return new CheckoutOrderCommand
        {
            AddressLine = createOrderDto.AddressLine,
            CardName = createOrderDto.CardName,
            CardNumber = createOrderDto.CardNumber,
            Country = createOrderDto.Country,
            Cvv = createOrderDto.Cvv,
            EmailAddress = createOrderDto.EmailAddress,
            Expiration = createOrderDto.Expiration,
            FirstName = createOrderDto.FirstName,
            LastName = createOrderDto.LastName,
            PaymentMethod = createOrderDto.PaymentMethod,
            State = createOrderDto.State,
            TotalPrice = createOrderDto.TotalPrice,
            UserName = createOrderDto.UserName,
            ZipCode = createOrderDto.ZipCode
        };
    }

    public static UpdateOrderCommand ToCommand(this OrderDto orderDto)
    {
        return new UpdateOrderCommand
        {
            Id = orderDto.Id,
            AddressLine = orderDto.AddressLine,
            CardName = orderDto.CardName,
            CardNumber = orderDto.CardNumber,
            Country = orderDto.Country,
            Cvv = orderDto.Cvv,
            EmailAddress = orderDto.EmailAddress,
            Expiration = orderDto.Expiration,
            FirstName = orderDto.FirstName,
            LastName = orderDto.LastName,
            PaymentMethod = orderDto.PaymentMethod,
            State = orderDto.State,
            TotalPrice = orderDto.TotalPrice,
            UserName = orderDto.UserName,
            ZipCode = orderDto.ZipCode
        };
    }
    public static void MapUpdate(this Order orderToUpdate,
                UpdateOrderCommand orderCommand)
    {
        orderToUpdate.AddressLine = orderCommand.AddressLine;
        orderToUpdate.CardName = orderCommand.CardName;
        orderToUpdate.CardNumber = orderCommand.CardNumber;
        orderToUpdate.Country = orderCommand.Country;
        orderToUpdate.Cvv = orderCommand.Cvv;
        orderToUpdate.EmailAddress = orderCommand.EmailAddress;
        orderToUpdate.Expiration = orderCommand.Expiration;
        orderToUpdate.FirstName = orderCommand.FirstName;
        orderToUpdate.LastName = orderCommand.LastName;
        orderToUpdate.PaymentMethod = orderCommand.PaymentMethod;
        orderToUpdate.State = orderCommand.State;
        orderToUpdate.TotalPrice = orderCommand.TotalPrice;
        orderToUpdate.UserName = orderCommand.UserName;
        orderToUpdate.ZipCode = orderCommand.ZipCode;
    }

    public static CheckoutOrderCommand ToCheckoutOrderCommand(this BasketCheckoutEvent message)
    {
        return new CheckoutOrderCommand
        {
            UserName = message.UserName!,
            AddressLine = message.AddressLine!,
            CardName = message.CardName!,
            CardNumber = message.CardNumber!,
            Country = message.Country!,
            Cvv = message.Cvv!,
            EmailAddress = message.EmailAddres!,
            Expiration = message.Expiration!,
            FirstName = message.FirstName!,
            LastName = message.LastName!,
            PaymentMethod = message.PaymentMethod ?? 0,
            State = message.State!,
            TotalPrice = message.TotalPrice!,
            ZipCode = message.ZipCode!
        };
    }
}

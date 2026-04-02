using OrderService.Entities;

namespace OrderService.Repositories;

public interface IOrderRepository : IRepository<Order>
{
    Task<IEnumerable<Order>> GetOrdersByUserName(string userName);
}

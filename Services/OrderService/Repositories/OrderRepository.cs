using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Entities;

namespace OrderService.Repositories;

public sealed class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(OrderContext dbContext) : base(dbContext)
    {
    }


    public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
    {
        var orderList = await _dbContext.Orders
                        .AsNoTracking()
                        .Where(o => o.UserName == userName)
                        .ToListAsync();
        return orderList;
    }

}

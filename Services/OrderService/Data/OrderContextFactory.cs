using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace OrderService.Data;

public sealed class OrderContextFactory : IDesignTimeDbContextFactory<OrderContext>
{
    public OrderContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        
        var connectionString= configuration.GetConnectionString("OrderServiceConnectionString");
        var optionsBuilder=new DbContextOptionsBuilder<OrderContext>();
        optionsBuilder.UseSqlServer(connectionString);
        return new OrderContext(optionsBuilder.Options);
    }

}

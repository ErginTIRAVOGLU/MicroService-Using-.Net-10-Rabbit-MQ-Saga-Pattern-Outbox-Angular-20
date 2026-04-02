using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Repositories;

namespace OrderService.Extensions;

public static class InfraServices
{
    public static IServiceCollection AddInfraServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<OrderContext>(options => options.UseSqlServer(
            configuration.GetConnectionString("OrderServiceConnectionString"),
            sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()
        ));
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IOrderRepository, OrderRepository>();
        return services;
    }
}

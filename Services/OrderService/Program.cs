using Common.Logging;
using EventBus.Messages.Common;
using MassTransit;
using OrderService.Consumer;
using OrderService.Data;
using OrderService.Dispatcher;
using OrderService.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(Logging.ConfigureLogger);


builder.Services.AddAuthorization();
builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

// Application Services
builder.Services.AddApplicationServices();

// Infra Services
builder.Services.AddInfraServices(builder.Configuration);

// Register OutboxMessageDispatcher as a hosted service
builder.Services.AddHostedService<OutboxMessageDispatcher>();

// Mass transit
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<BasketOrderingConsumer>();
    config.AddConsumer<PaymentFailedConsumer>();
    config.AddConsumer<PaymentCompletedConsumer>();
    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
        cfg.ReceiveEndpoint(EventBusConstant.BasketCheckoutQueue, c =>
        {
            c.ConfigureConsumer<BasketOrderingConsumer>(ctx);
        });
        cfg.ReceiveEndpoint(EventBusConstant.PaymentFailedQueue, c =>
       {
              c.ConfigureConsumer<PaymentFailedConsumer>(ctx);
          });
        cfg.ReceiveEndpoint(EventBusConstant.PaymentCompletedQueue, c =>
      {
              c.ConfigureConsumer<PaymentCompletedConsumer>(ctx);
          });
    });
});

var app = builder.Build();

app.MigrateDatabase<OrderContext>((context, services) =>
{
    var logger = services.GetService<ILogger<OrderContextSeed>>();
    OrderContextSeed.SeedAsync(context, logger!).Wait();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


using EventBus.Messages.Common;
using MassTransit;
using OrderService.Data;
using OrderService.EventBusConsumer;
using OrderService.Extensions;

var builder = WebApplication.CreateBuilder(args);

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

// Mass transit
builder.Services.AddMassTransit(config =>
{
   config.AddConsumer<BasketOrderingConsumer>();
   config.UsingRabbitMq((ctx,cfg) =>
   {
       cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
       cfg.ReceiveEndpoint(EventBusConstant.BasketCheckoutQueue, c=>
       {
           c.ConfigureConsumer<BasketOrderingConsumer>(ctx);
       });
   }); 
});

var app = builder.Build();

app.MigrateDatabase<OrderContext>((context,services)=>
{
   var logger= services.GetService<ILogger<OrderContextSeed>>();
   OrderContextSeed.SeedAsync(context,logger!).Wait(); 
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


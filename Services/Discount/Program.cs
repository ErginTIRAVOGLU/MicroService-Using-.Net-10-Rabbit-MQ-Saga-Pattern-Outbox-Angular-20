using System.Reflection;
using Common.Logging;
using Discount.Extensions;
using Discount.Handlers;
using Discount.Repositories;
using Discount.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
 
builder.Host.UseSerilog(Logging.ConfigureLogger);



builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();


//-Swagger
builder.Services.AddEndpointsApiExplorer();
 

//-Mediatr
var assemblies = new Assembly[]
{
    Assembly.GetExecutingAssembly(), 
    typeof(CreateDiscountHandler).Assembly
};

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));
builder.Services.AddGrpc();

var app = builder.Build();

app.MigrateDatabase<Program>();
app.UseRouting();
app.MapGrpcService<DiscountService>();


app.Run();

 
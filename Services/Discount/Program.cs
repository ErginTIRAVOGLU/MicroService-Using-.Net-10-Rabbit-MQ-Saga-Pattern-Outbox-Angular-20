using System.Reflection;
using Discount.Extensions;
using Discount.Handlers;
using Discount.Repositories;
using Discount.Services;

var builder = WebApplication.CreateBuilder(args);
 



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

 
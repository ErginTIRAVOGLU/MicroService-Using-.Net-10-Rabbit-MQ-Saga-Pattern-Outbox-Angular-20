using System.Reflection;
using Catalog.Data;
using Catalog.Handlers.Brands;
using Catalog.Repositories;
using Common.Logging;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(Logging.ConfigureLogger);

BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));


// Program.cs
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    return new MongoClient(config["DatabaseSettings:ConnectionStrings"]);
});

builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ITypeRepository, TypeRepository>();

builder.Services.AddScoped<DatabaseSeeder>();

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//-Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//-Mediatr
var assemblies = new Assembly[]
{
    Assembly.GetExecutingAssembly(),
    typeof(GetAllBrandsHandler).Assembly
};

builder.Services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(assemblies));




var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
    await seeder.SeedAsync(app.Configuration);
}

 
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


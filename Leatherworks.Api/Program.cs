using Leatherworks.Api.Entities;
using MongoDB.Bson;
using MongoDB.Driver;



var builder = WebApplication.CreateBuilder(args);

// Load the configuration
builder.Configuration.AddJsonFile("appsettings.json");
var configuration = builder.Configuration;

string ipAddress = configuration["LeatherworksDatabase:ConnectionString"];

builder.Services.Configure<LeatherworksDatabaseSettings>(
    builder.Configuration.GetSection("LeatherworksDatabase"));


builder.Services.AddSingleton<MongoClient>(_ => new MongoClient(ipAddress));

builder.Services.AddSingleton<IMongoDatabase>(
    provider => provider.GetRequiredService<MongoClient>().GetDatabase("Leatherworks"));

builder.Services.AddSingleton<IMongoCollection<Product>>(
    provider => provider.GetRequiredService<IMongoDatabase>().GetCollection<Product>("Products"));

var app = builder.Build();

// app.MapGet("/products", () => products);

app.MapGet("/products", async (IMongoCollection<Product> collection)
    => TypedResults.Ok(await collection.Find(Builders<Product>.Filter.Empty).ToListAsync()));

app.MapGet("/products/{productId}", async (IMongoCollection<Product> collection, int productId)
    =>
{
    var offices = await collection.Find(
            Builders<Product>.Filter.Eq("Id", productId))
        .FirstOrDefaultAsync();
    return TypedResults.Ok(offices);
});

app.Run();

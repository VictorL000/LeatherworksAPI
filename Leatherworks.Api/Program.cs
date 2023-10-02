using Leatherworks.Api.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

    
List<Product> products = new()
{
    new Product()
    {
        Id = 1,
        title = "Note Sleeve",
        price = 199.99M,
        images = new[] {  "https://bellroy-product-images.imgix.net/bellroy_dot_com_gallery_image/CAD/WNSD-ARA-113/5?w=730&h=487&fit=clip&dpr=1&q=75&auto=format", "https://bellroy-product-images.imgix.net/bellroy_dot_com_gallery_image/CAD/WNSD-ARA-113/0?w=730&h=487&fit=clip&dpr=1&q=75&auto=format" },
        description = "Balancing spacious and slim, this upgraded all-rounder’s innovative leather, and custom finishing details add to its refined look.",
        category = "Bifold Wallet",
        features = new[] { "4 - 11 cards", "Flat bills and coins", "Premium leather"},
        colors = new[]
        {
            Tuple.Create("Hazelnut", "#a8715a"),
            Tuple.Create("Dazelnut", "#b8715a"),
        },
    },
    new Product()
    {
        Id = 2,
        title = "Note Sleeve Premium",
        price = 299.99M,
        images = new[] {  "https://bellroy-product-images.imgix.net/bellroy_dot_com_gallery_image/CAD/WNSD-ARA-113/5?w=730&h=487&fit=clip&dpr=1&q=75&auto=format", "https://bellroy-product-images.imgix.net/bellroy_dot_com_gallery_image/CAD/WNSD-ARA-113/0?w=730&h=487&fit=clip&dpr=1&q=75&auto=format" },
        description = "Balancing spacious and slim, this upgraded all-rounder’s innovative leather, and custom finishing details add to its refined look.",
        category = "Bifold Wallet",
        features = new[] { "4 - 11 cards", "Flat bills and coins", "Premium leather"},
        colors = new[]
        {
            Tuple.Create("Hazelnut", "#a8715a"),
            Tuple.Create("Dazelnut", "#b8715a"),
        },
    }
};



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<MongoClient>(_ => new MongoClient());

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

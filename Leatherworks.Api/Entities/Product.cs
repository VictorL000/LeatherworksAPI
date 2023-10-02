using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Leatherworks.Api.Entities;
public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? id_mongo { get; set; }

    [BsonElement("Id")]
    public int Id { get; set; }
    [BsonElement("title")]
    public required string title { get; set; }
    [BsonElement("price")]
    public required decimal price { get; set; }
    [BsonElement("images")]
    public required string[] images { get; set; }
    [BsonElement("description")]
    public required string description { get; set; }
    [BsonElement("category")]
    public required string category { get; set; }
    [BsonElement("features")]
    public string[] features { get; set; } = { "11 cards", "RFID Blocking" };
    [BsonElement("colors")]
    public Tuple<string, string>[] colors { get; set; } = {
            Tuple.Create("Hazelnut", "#a8715a"),
            Tuple.Create("bazelnut", "#b8715a"),
        };
}
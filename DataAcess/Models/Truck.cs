using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace DataAccess.Models;

public record Truck
{
    [BsonId]
    public int Id { get; set; }

    [BsonElement]
    public string Model { get; set; }

    [BsonElement]
    public Brand Brand { get; set; }

    [BsonElement]
    public MyColor MyColor { get; set; }

    [BsonElement]
    public int HorsePower { get; set; }
}
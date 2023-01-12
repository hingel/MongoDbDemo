using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace DataAccess.Models;

public record Car
{
    [BsonId]
    public ObjectId Id { get; set; }

    [BsonElement]
    public string Model { get; set; }

    [BsonElement]
    public Make Make { get; set; }

    [BsonElement]
    public Color Color { get; set; }

    [BsonElement]
    public int HorsePower { get; set; }
}
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataAccess.Models;

public class Color
{
    [BsonId]
    public ObjectId Id { get; set; }
    [BsonElement]
    public string CarColor { get; set; }
}
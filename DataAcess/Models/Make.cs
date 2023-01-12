using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataAccess.Models;


public record Make
{
    [BsonId]
    public ObjectId Id { get; set; }
    [BsonElement]
    public string Name { get; set; }

}
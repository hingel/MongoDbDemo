using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataAccess.Models;


public class Make
{
    [BsonId]
    public ObjectId Id { get; set; }
    [BsonElement]
    public string MakeName { get; set; }

}
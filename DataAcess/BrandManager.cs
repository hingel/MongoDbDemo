using System.Runtime.InteropServices;
using DataAccess.Models;
using MongoDB.Driver;

namespace DataAccess;

public class BrandManager : IRepository<Brand>
{
    private readonly IMongoCollection<Brand> _brandCollection;

    public BrandManager()
    {
        var hostname = "localhost";
        var databaseName = "demo";
        var connectionstring = $"mongodb://{hostname}:27017";

        var client = new MongoClient(connectionstring);
        var database = client.GetDatabase(databaseName);

        _brandCollection = database.GetCollection<Brand>("MyBrand", new MongoCollectionSettings() { AssignIdOnInsert = true });
    }

    public void Add(Brand entity)
    {
        _brandCollection.InsertOne(entity);
    }
    
    public IEnumerable<Brand> GetAllItems()
    {
        return _brandCollection.Find(_ => true).ToList();
    }

    public Brand GetById(object id)
    {
        var filter = Builders<Brand>.Filter.Eq("Id", id);

        return _brandCollection.Find(filter).FirstOrDefault();
    }

    public void Replace(object id, Brand item)
    {
        var filter = Builders<Brand>.Filter.Eq("Id", id);
        var update = Builders<Brand>.Update.Set("Name", item.Name);

        _brandCollection.FindOneAndUpdate(filter, update, new FindOneAndUpdateOptions<Brand, Brand>() {IsUpsert = true});
    }
    public void Delete(object id)
    {
        var filter = Builders<Brand>.Filter.Eq("Id", id);

        _brandCollection.FindOneAndDelete(filter);
    }
}
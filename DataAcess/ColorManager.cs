using DataAccess.Models;
using MongoDB.Driver;

namespace DataAccess;

public class ColorManager : IRepository<MyColor>
{
    private readonly IMongoCollection<MyColor> _colorCollection;

    public ColorManager()
    {
        var hostname = "localhost";
        var databaseName = "demo";
        var connectionstring = $"mongodb://{hostname}:27017";

        var client = new MongoClient(connectionstring);
        var database = client.GetDatabase(databaseName);

        _colorCollection = database.GetCollection<MyColor>("Mycolors", new MongoCollectionSettings() { AssignIdOnInsert = true });
    }

    public void Add(MyColor entity)
    {
        _colorCollection.InsertOne(entity);
    }

    public IEnumerable<MyColor> GetAllItems()
    {
        return _colorCollection.Find(_ => true).ToEnumerable();
    }

    public MyColor GetById(Object id)
    {
        var filter = Builders<MyColor>.Filter.Eq("Id", id);

        return _colorCollection.Find(filter).FirstOrDefault();
    }

    public void Replace(object id, MyColor item)
    {
        var filter = Builders<MyColor>.Filter.Eq("Id", id);
        var update = Builders<MyColor>.Update.Set("CarColor", item.CarColor);

        _colorCollection.FindOneAndUpdate(filter, update,
            new FindOneAndUpdateOptions<MyColor, MyColor>() { IsUpsert = true });
    }
    public void Delete(object id)
    {
        var filter = Builders<MyColor>.Filter.Eq("Id", id);

        _colorCollection.DeleteOne(filter);
    }
}
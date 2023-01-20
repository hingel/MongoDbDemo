using DataAccess.Models;
using MongoDB.Driver;

namespace DataAccess
{
    public class CarManager : IRepository<Car>
    {

        private readonly IMongoCollection<Car> _collection;

        public CarManager()
        {
            var hostname = "localhost";
            var databaseName = "demo";
            var connectionstring = $"mongodb://{hostname}:27017";

            var client = new MongoClient(connectionstring);
            var database = client.GetDatabase(databaseName);

            //Inkopierat ifrån Atlas.
            //var settings = MongoClientSettings.FromConnectionString("mongodb+srv://Test:<wqJhdIoGImbvLGoO>@cluster0.joo7uio.mongodb.net/?retryWrites=true&w=majority");
            //var client = new MongoClient(settings);
            //var database = client.GetDatabase("test");

            _collection = database.GetCollection<Car>("cars", new MongoCollectionSettings() { AssignIdOnInsert = true });
        }

        public void Add(Car carToAdd)
        {
            _collection.InsertOne(carToAdd);
        }
        
        public IEnumerable<Car> GetAllItems()
        {
            var col = _collection.Find(_ => true);
            return col.ToEnumerable();
        }

        public Car GetById(object id)
        {
            var filter = Builders<Car>.Filter.Eq("Id", id);

            return _collection.Find(filter).FirstOrDefault();
        }

        public void Replace(object id, Car newCar)
        {
            var filter = Builders<Car>.Filter.Eq("Id", id);
            var update = Builders<Car>.Update
                .Set("Model", newCar.Model)
                .Set("Brand", newCar.Model)
                .Set("MyColor", newCar.MyColor)
                .Set("HorsePower", newCar.HorsePower);

            _collection.FindOneAndUpdate(filter, update, new FindOneAndUpdateOptions<Car, Car> { IsUpsert = true });
        }

        public void Delete(object idToDelete)
        {
            var filter = Builders<Car>.Filter.Eq("Id", idToDelete);
            _collection.FindOneAndDelete(filter);
        }
    }
}
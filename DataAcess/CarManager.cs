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
            _collection =
                database.GetCollection<Car>("cars", new MongoCollectionSettings() { AssignIdOnInsert = true });
        }

        public void Add(Car carToAdd)
        {
            _collection.InsertOne(carToAdd);
        }

        public IEnumerable<Car> GetAll()
        {
            return _collection.Find(_ => true).ToEnumerable();
        }

        public IEnumerable<Car> GetByMake(string make)
        {
            return _collection.Find(c => (!string.IsNullOrEmpty(make) && c.Make == make)).ToEnumerable();
        }

        public void Replace(object id, Car newCar)
        {
            var filter = Builders<Car>.Filter.Eq("Id", id);
            var update = Builders<Car>.Update
                .Set("Model", newCar.Model)
                .Set("Make", newCar.Model)
                .Set("Color", newCar.Color)
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
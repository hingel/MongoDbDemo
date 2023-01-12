using DataAccess.Models;
using MongoDB.Driver;

namespace DataAccess
{
    public class CarManager : IRepository<Car>
    {

        private readonly IMongoCollection<Car> _collection;
        private readonly IMongoCollection<Color> _colorCollection;
        private readonly IMongoCollection<Make> _makeCollection;

        public CarManager()
        {
            var hostname = "localhost";
            var databaseName = "demo";
            var connectionstring = $"mongodb://{hostname}:27017";

            var client = new MongoClient(connectionstring);
            var database = client.GetDatabase(databaseName);
            _collection = database.GetCollection<Car>("cars", new MongoCollectionSettings() { AssignIdOnInsert = true });
            _colorCollection = database.GetCollection<Color>("color", new MongoCollectionSettings(){AssignIdOnInsert = true});
            _makeCollection = database.GetCollection<Make>("make", new MongoCollectionSettings(){AssignIdOnInsert = true});
        }

        public void Add(Car carToAdd)
        {
            _collection.InsertOne(carToAdd);
        }

        public void AddColor(Color colorToAdd)
        {
            _colorCollection.InsertOne(colorToAdd);
        }

        public void AddMake(Make makeToAdd)
        {
            _makeCollection.InsertOne(makeToAdd);
        }

        public IEnumerable<Car> GetAllCars()
        {
            var col = _collection.Find(_ => true);
            return col.ToEnumerable();
        }

        public IEnumerable<Color> GetAllColors()
        {
            var col = _colorCollection.Find(_ => true);
            return col.ToEnumerable();
        }

        //Risk att det nedan är helt fel:
        public IEnumerable<Car> GetBySpec(Car car)
        {
            return _collection.Find(c => (c.Make == car.Make)).ToEnumerable();
            //return _collection.Find(c => (!string.IsNullOrEmpty(make) && c.Make == make)).ToEnumerable();
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
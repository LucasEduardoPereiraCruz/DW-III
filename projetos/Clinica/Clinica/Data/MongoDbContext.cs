using MongoDB.Driver;
using Clinica.Models;

namespace Clinica.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            _database = client.GetDatabase("ClinicaDB");
        }

        public IMongoCollection<ClinicaModel> Clinicas =>
            _database.GetCollection<ClinicaModel>("Clinicas");
    }
}
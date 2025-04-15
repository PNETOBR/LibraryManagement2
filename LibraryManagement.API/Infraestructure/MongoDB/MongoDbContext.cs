using LibraryManagement.API.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LibraryManagement.API.Infraestructure.MongoDB;

//public class MongoDBContext
//{
//    private readonly IMongoDatabase _database;

//    public MongoDBContext(IOptions<MongoDBSettings> settings)
//    {
//        var client = new MongoClient(settings.Value.ConnectionString);
//        _database = client.GetDatabase(settings.Value.DatabaseName);
//    }

//    public IMongoCollection<Books> Books => _database.GetCollection<Books>("Books");
//}

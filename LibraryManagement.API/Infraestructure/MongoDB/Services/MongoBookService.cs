using LibraryManagement.API.Infraestructure.MongoDB;
using LibraryManagement.API.ViewModels;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LibraryManagement.API.Infraestructure.MongoDB.Services;

//public class MongoBookService
//{
//    private readonly IMongoCollection<MongoBook> _booksCollection;

//    public MongoBookService(IOptions<MongoDBSettings> settings, MongoClient mongoClient)
//    {
//        var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
//        _booksCollection = database.GetCollection<MongoBook>(settings.Value.CollectionName);
//    }

//    public async Task<List<MongoBook>> GetAllBooksAsync() =>
//        await _booksCollection.Find(book => true).ToListAsync();

//    public async Task AddBookAsync(MongoBook book) =>
//        await _booksCollection.InsertOneAsync(book);
//}

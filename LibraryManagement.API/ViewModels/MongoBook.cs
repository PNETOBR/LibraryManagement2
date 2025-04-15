using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace LibraryManagement.API.ViewModels
{
    public class MongoBook
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public int Year { get; set; }
    }
}

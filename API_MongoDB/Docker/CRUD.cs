using MongoDB.Bson;
using MongoDB.Driver;

static class CRUD {
    public static void Create(IMongoCollection<BsonDocument> books, Book book) {
        books.InsertOne(book.ToBsonDocument());
    }

    public static void Read() {
        // ...
    }

    public static void Update() {
        // ...
    }

    public static void Delete() {
        // ...
    }
}
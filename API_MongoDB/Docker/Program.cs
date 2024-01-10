using System.Collections;
using System.Reflection;
using MongoDB.Bson;
using MongoDB.Driver;

void PrintProperties(object obj, string prefix = "")
{
    Type type = obj.GetType();
    PropertyInfo[] properties = type.GetProperties();

    foreach (PropertyInfo property in properties)
    {
        if (property!=null) {
            object propValue = property.GetValue(obj, null);
            if (propValue!=null) {
                Console.WriteLine($"{prefix}{property.Name} = {propValue}");
                Console.WriteLine();
                if (property.Name == "Databases") {
                    foreach (var db in (IEnumerable)propValue) {
                        PrintProperties(db, prefix + "  ");
                    }
                }
            }
        }
        
    }
}

MongoClient client = Connection.Connect();
// PrintProperties(client);

Book book = new Book() {
    BookName = "The C# Programming Language",
    Price = 9.99m,
    Category = "Programming",
    Author = "Anders Hejlsberg"
};

IMongoDatabase database = client.GetDatabase("booksdb");
PrintProperties(database);

IMongoCollection<BsonDocument> booksColl = database.GetCollection<BsonDocument>("books");
PrintProperties(booksColl);

CRUD.Create(booksColl, book); 





using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Libmongocrypt;

namespace API.Classes.Model
{
    public class Canco
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get ; set; } = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
        public Binary cancion {get; set;} = null!;
    }
}
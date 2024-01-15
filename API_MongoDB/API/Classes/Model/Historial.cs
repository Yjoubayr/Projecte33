using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API.Classes.Model
{
    public class Historial
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
        [BsonElement("IDDispositiu")]
        public string IDDispositiu {get; set;} = null!;
        public ICollection<Canco> canco {get; set;} = null!;

    }
}
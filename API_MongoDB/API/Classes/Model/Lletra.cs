using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API.Classes.Model
{
    public class Lletra
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
        [BsonElement("IDLletra")]
        public string IDLletra {get; set;} = null!;
        [BsonElement("IDCanco")]
        public string IDCanco {get; set;} = null!;
        [BsonElement("ContingutLletra")]
        public string ContingutLletra {get; set;} = null!;
        
    }
}
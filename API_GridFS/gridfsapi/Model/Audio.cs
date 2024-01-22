using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace gridfsapi.Model
{
    
    public class Audio
    {
        [BsonId]
        public ObjectId id {get; set;}
        public required string IdCanco {get; set;}

        public required byte[] Contingut {get; set;}
    }
}
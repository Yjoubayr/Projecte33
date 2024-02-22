using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace gridfsapi
{
    public class Album
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string _ID { get; set; } = MongoDB.Bson.ObjectId.GenerateNewId().ToString();

        public int Any { get; set; }
        public string Titol { get; set; }
        public string Genere { get; set; }
        public string UIDSong { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId ImatgePortadaeId { get; set; }
         public ObjectId ImatgeContraPortadaId { get; set; }

    }


}
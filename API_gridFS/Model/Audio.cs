using Microsoft.AspNetCore.Routing.Constraints;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Security.Policy;

namespace gridfsapi
{

    /// <summary>
    /// Representa un objecte d'àudio.
    /// </summary>
    public class Audio
    {
        /// <summary>
        /// Obté o estableix l'identificador de l'àudio.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _ID { get; set; } = MongoDB.Bson.ObjectId.GenerateNewId().ToString();

        /// <summary>
        /// Obté o estableix l'identificador de la cançó en SQL.
        /// </summary>
        [BsonElement("UID")]
        public string IdCanco_SQL { get; set; }

        /// <summary>
        /// Obté o estableix l'identificador de fitxer d'àudio.
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId AudioFileId { get; set; }
    }
}
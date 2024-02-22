using Microsoft.AspNetCore.Routing.Constraints;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Security.Policy;

namespace gridfsapi
{

    /// <summary>
    /// Representa un objecte d'�udio.
    /// </summary>
    public class Audio
    {
        /// <summary>
        /// Obt� o estableix l'identificador de l'�udio.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _ID { get; set; } = MongoDB.Bson.ObjectId.GenerateNewId().ToString();

        /// <summary>
        /// Obt� o estableix l'identificador de la can�� en SQL.
        /// </summary>
        [BsonElement("UID")]
        public string IdCanco_SQL { get; set; }

        /// <summary>
        /// Obt� o estableix l'identificador de fitxer d'�udio.
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId AudioFileId { get; set; }
    }
}
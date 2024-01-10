using MongoDB.Bson.Serialization.Attributes;

namespace API.Classes.Model
{
    public class Historial
    {
        [BsonId]
        public string IDDispositiu {get; set;} = null!;
        public ICollection<Canco> canco {get; set;} = null!;

    }
}
using MongoDB.Bson.Serialization.Attributes;

namespace API.Classes.Model
{
    public class Canco
    {
        [BsonId]
        public string ID {get; set;} = null!;
           
    }
}
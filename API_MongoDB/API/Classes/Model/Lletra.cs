using MongoDB.Bson.Serialization.Attributes;

namespace API.Classes.Model
{
    public class Lletra
    {
        [BsonId]
        public string IDLletra {get; set;} = null!;
        public string IDCanco {get; set;} = null!;
        public string ContingutLletra {get; set;} = null!;
        
    }
}
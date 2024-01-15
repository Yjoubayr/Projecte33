namespace API.Classes.Model
{
    public class MongoDBSettings{
        public string ConnectionString{get; set;} = null!;
        public string DatabaseName{get; set;} = null!;
        public string CancoCollectionName{get; set;} = null!;
        public string LletraCollectionName{get; set;} = null!;
        public string HistorialCollectionName{get; set;} = null!;
    }
}
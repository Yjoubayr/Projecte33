namespace API.Classes.Model
{
    public class ReproMusicaDBSettings{
        public string ConnectionString{get; set;} = null!;
        public string DatabaseName{get; set;} = null!;
        public string CollectionName{get; set;} = null!;
    }
}
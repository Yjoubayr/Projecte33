namespace API.Classes.Model
{
    public class MongoDBSettings{
        MongoDBSettings() {
            using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
            ILogger logger = factory.CreateLogger("Program");
            logger.LogInformation("Hello World! Logging is {Description}.", "fun");
        }
        public string ConnectionString{get; set;} = null!;
        public string DatabaseName{get; set;} = null!;
        public string CancoCollectionName{get; set;} = null!;
        public string LletraCollectionName{get; set;} = null!;
        public string HistorialCollectionName{get; set;} = null!;
    }
}
using MongoDB.Driver;

public static class Connection
{
    public static MongoDB.Driver.MongoClient Connect()
    {
        var mongoClientSettings = new MongoClientSettings
            {
                /* Credential = MongoCredential.CreateCredential("booksdb", "ubooks", "a"),
                Server = new MongoServerAddress("localhost", 27017) */

                Credential = MongoCredential.CreateCredential("admin", "root", "a"),
                Server = new MongoServerAddress("localhost", 27017)
            };
        mongoClientSettings.AllowInsecureTls = true;
            
        return new MongoDB.Driver.MongoClient(mongoClientSettings);
    }
    
}

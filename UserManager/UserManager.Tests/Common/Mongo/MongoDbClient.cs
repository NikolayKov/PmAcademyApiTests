using MongoDB.Driver;

namespace UserManager.Tests.Common.Mongo
{
    public class MongoDbClient
    {
        private static IMongoDatabase _instance;
        private static readonly object _syncRoot = new object();

        protected MongoDbClient() { }

        public static IMongoDatabase GetInstance()
        {
            if (_instance == null)
            {
                lock (_syncRoot)
                {
                    if (_instance == null)
                    {
                        string connectionString = "mongodb://localhost:27017";
                        var client = new MongoClient(connectionString);
                        _instance = client.GetDatabase("UserManager");
                    }

                }
            }

            return _instance;
        }
    }
}

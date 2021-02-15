using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using UserManager.Models;

namespace UserManager.Clients
{
    public class MongoClient
    {
        private readonly IMongoDatabase _db;

        public MongoClient(MongoDB.Driver.MongoClient client)
        {
            _db = client.GetDatabase("UserManager");
        }
        
        public async Task AddUser(UserDto user)
        {
            var collection = _db.GetCollection<UserDto>("Users");

            await collection.InsertOneAsync(user);
        }

        public UserDto GetUserById(string id)
        {
            var collection = _db.GetCollection<UserDto>("Users");

            var user = collection.AsQueryable().FirstOrDefault(u => u.Id == id);

            return user;
        }
    }
}

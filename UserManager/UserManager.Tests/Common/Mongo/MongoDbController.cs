using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserManager.Tests.Common.Mongo.Models;

namespace UserManager.Tests.Common.Mongo
{
    public class MongoDbController
    {
        public static void AddUser(UserDto user)
        {
            var collection = MongoDbClient.GetInstance().GetCollection<UserDto>("Users");

            collection.InsertOne(user);
        }
    }
}

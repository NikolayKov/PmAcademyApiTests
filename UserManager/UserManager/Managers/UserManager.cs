using System;
using System.Linq;
using UserManager.Clients;
using UserManager.Contracts.PublicApi;
using UserManager.Models;

namespace UserManager.Managers
{
    public class UserManager
    {
        private readonly ReqresApiClient _reqresApiClient;
        private readonly MongoClient _mongoClient;

        public UserManager(ReqresApiClient reqresApiClient, MongoClient mongoClient)
        {
            _reqresApiClient = reqresApiClient;
            _mongoClient = mongoClient;
        }

        public string AddUser(string email, string firstName, string lastName)
        {
            var user = new UserDto()
            {
                Id = Guid.NewGuid().ToString(),
                Email = email,
                FirstName = firstName,
                LastName = lastName
            };

            //Get data from external source

           var reqresUsers = _reqresApiClient.GetUsers().Result;
            if (reqresUsers.Any(ru => ru.Email == email))
            {
                var reqUser = reqresUsers.FirstOrDefault(ru => ru.Email == email);
                user.FirstName = reqUser.FirstName;
                user.LastName = reqUser.LastName;
                user.ExternalId = reqUser.Id;
            }

            try
            {
                _mongoClient.AddUser(user).Wait();
            }
            catch (AggregateException aggEx)
            {
                return null;
            }

            return user.Id;
        }

        public GetUserResponse GetUserById(string id)
        {
            var user = _mongoClient.GetUserById(id);

            if (user == null)
            {
                return null;
            }

            var response = new GetUserResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                Email = user.Email,
                LastName = user.LastName
            };

            if (user.ExternalId == null) return response;
            
            var externalUser = _reqresApiClient.GetUserById((int)user.ExternalId).Result;

            if (externalUser != null)
            {
                response.Avatar = externalUser.Avatar;
            }

            return response;
        }
    }
}

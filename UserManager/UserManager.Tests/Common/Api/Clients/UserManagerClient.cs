using Newtonsoft.Json;
using RestSharp;

namespace UserManager.Tests.Common.Api.Clients
{
    public class UserManagerClient
    {
        public static IRestResponse AddUser(string email, string firstName, string lastName)
        {
            var request = new RestRequest("api/user", Method.POST)
                .AddParameter("email", email, ParameterType.QueryString)
                .AddParameter("firstName", firstName, ParameterType.QueryString)
                .AddParameter("lastName", lastName, ParameterType.QueryString);

            var response = RestSharpClient.GetInstance().ExecuteAsync(request).Result;

            return response;
        }

        public static IRestResponse GetUserById(string id)
        {
            var request = new RestRequest($"api/user/{id}", Method.GET);

            var response = RestSharpClient.GetInstance().ExecuteAsync(request).Result;

            return response;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UserManager.Contracts.ExternalApi;

namespace UserManager.Clients
{
    public class ReqresApiClient
    {
        private readonly HttpClient _httpClient;

        public ReqresApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<User>> GetUsers()
        {
            var content = await GetContent("/api/users?page=2");

            var response = JsonConvert.DeserializeObject<UserListResponse>(content);

            return response.Users;
        }

        public async Task<User> GetUserById(int id)
        {
            string content = "";
            try
            {
                content = GetContent($"/api/users/{id}").Result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            var response = JsonConvert.DeserializeObject<UserResponse>(content);

            return response.User;
        }

        private async Task<string> GetContent(string requestPath)
        {
            var httpResponse = await _httpClient.GetAsync(requestPath);

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve tasks");
            }

            var content = await httpResponse.Content.ReadAsStringAsync();

            return content;
        }
    }
}

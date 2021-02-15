using System.Collections.Generic;
using Newtonsoft.Json;

namespace UserManager.Contracts.ExternalApi
{
    public class UserListResponse
    {
        [JsonProperty("data")]
        public List<User> Users { get; set; }
    }
}

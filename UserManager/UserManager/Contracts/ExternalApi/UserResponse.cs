using Newtonsoft.Json;

namespace UserManager.Contracts.ExternalApi
{
    public class UserResponse
    {
        [JsonProperty("data")]
        public User User { get; set; }
    }
}

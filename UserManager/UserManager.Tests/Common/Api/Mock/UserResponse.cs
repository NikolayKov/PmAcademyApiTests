using Newtonsoft.Json;

namespace UserManager.Tests.Common.Api.Mock
{
    public class UserResponse
    {
        [JsonProperty("data")]
        public User User { get; set; }
    }
}

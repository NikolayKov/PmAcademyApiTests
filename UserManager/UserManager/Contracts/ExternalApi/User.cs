﻿using Newtonsoft.Json;

namespace UserManager.Contracts.ExternalApi
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        public string Avatar { get; set; }
    }
}

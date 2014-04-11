using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HipperChat.Core.Authentication
{
    public class Token
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresInSeconds { get; set; }

        [JsonProperty("group_name")]
        public string GroupName { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        public string Scope { get; set; }

        [JsonProperty("group_id")]
        public int GroupId { get; set; }
    }
}

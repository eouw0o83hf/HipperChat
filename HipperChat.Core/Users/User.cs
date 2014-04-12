using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HipperChat.Core.Users
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Links Links { get; set; }

        [JsonProperty("mention_name")]
        public string MentionName { get; set; }

        public bool IsDeleted { get; set; }
    }
}

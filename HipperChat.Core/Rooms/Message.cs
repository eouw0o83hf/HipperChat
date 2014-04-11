using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HipperChat.Core.Rooms
{
    public class Message
    {
        public string Color { get; set; }

        [JsonProperty("Message")]
        public string Text { get; set; }

        public bool? Notify { get; set; }

        [JsonIgnore]
        public MessageFormatEnum? MessageFormat { get; set; }

        [JsonProperty("Message_Format")]
        public string MessageFormatString
        {
            get
            {
                return MessageFormat.HasValue ? MessageFormat.Value.ToString().ToLowerInvariant() : null;
            }
        }

    }

    public enum MessageFormatEnum
    {
        Html,
        Text
    }
}

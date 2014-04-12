using HipperChat.Core.Users;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HipperChat.Core.Rooms
{
    public class IncomingChat
    {
        public string Color { get; set; }
        public DateTimeOffset Date { get; set; }

        [JsonConverter(typeof(UserFromNameConverter))]
        public User From { get; set; }
        public Guid Id { get; set; }

        public ICollection<User> Mentions { get; set; }

        public string Message { get; set; }

        [JsonProperty("message_format")]
        public string MessageFormatString
        {
            set
            {
                MessageFormatEnum result;
                if (Enum.TryParse<MessageFormatEnum>(value, out result))
                {
                    MessageFormat = result;
                }
                else
                {
                    MessageFormat = null;
                }
            }
        }

        public MessageFormatEnum? MessageFormat { get; set; }

        private class UserFromNameConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return true;
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                if (reader.ValueType == typeof(string))
                {
                    return new User
                    {
                        Name = reader.Value.ToString()
                    };
                }

                var jObject = JObject.Load(reader);

                var result = new User();
                serializer.Populate(jObject.CreateReader(), result);
                return result;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }
    }
}

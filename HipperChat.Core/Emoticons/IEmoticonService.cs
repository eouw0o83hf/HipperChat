using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HipperChat.Core.Emoticons
{
    public interface IEmoticonService
    {
        ICollection<Emoticon> GetEmoticons();
        EmoticonMetadata GetEmoticon(string shortcut);
    }

    public class EmoticonService : IEmoticonService
    {
        private readonly string _apiKey;

        public EmoticonService(string apiKey)
        {
            _apiKey = apiKey;
        }

        public ICollection<Emoticon> GetEmoticons()
        {
            var client = new WebClient();
            var results = new List<Emoticon>();
            var sources = new[] { EmoticonSource.Global, EmoticonSource.Group };

            foreach (var source in sources)
            {
                for (var i = 0; ; ++i)
                {
                    var json = client.DownloadString("https://api.hipchat.com/v2/emoticon?auth_token=" + _apiKey + "&start-index=" + (i * 100) + "&type=" + source.ToString().ToLowerInvariant());
                    var response = JsonConvert.DeserializeObject<GenericResult<Emoticon>>(json);

                    foreach (var item in response.Items)
                    {
                        item.Source = source;
                    }
                    
                    results.AddRange(response.Items);

                    if (response.Items.Count < 100)
                    {
                        break;
                    }
                }
            }

            return results;
        }

        public EmoticonMetadata GetEmoticon(string shortcut)
        {
            try
            {
                var client = new WebClient();
                var json = client.DownloadString("https://api.hipchat.com/v2/emoticon/" + shortcut + "?auth_token=" + _apiKey);
                return JsonConvert.DeserializeObject<EmoticonMetadata>(json);
            }
            catch
            {
                // No such emoticon
                return null;
            }
        }
    }
}

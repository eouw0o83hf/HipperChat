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
        ICollection<Emoticon> GetEmoticons(string apiKey);
        EmoticonMetadata GetEmoticon(string apiKey, string shortcut);
    }

    public class EmoticonService : BaseService, IEmoticonService
    {
        public ICollection<Emoticon> GetEmoticons(string apiKey)
        {
            var client = new WebClient();
            var results = new List<Emoticon>();
            var sources = new[] { EmoticonSource.Global, EmoticonSource.Group };

            foreach (var source in sources)
            {
                for (var i = 0; ; ++i)
                {
                    var parameters = new Dictionary<string, string>
                    {
                        { "start-index", (i * 100).ToString() },
                        { "type", source.ToString().ToLowerInvariant() }
                    };

                    var response = GetActionResult<GenericResult<Emoticon>>("emoticon", apiKey, parameters);

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

        public EmoticonMetadata GetEmoticon(string apiKey, string shortcut)
        {
            var command = string.Format("emoticon/{0}", shortcut);
            try
            {
                return GetActionResult<EmoticonMetadata>(command, apiKey);
            }
            catch
            {
                // No such emoticon
                return null;
            }
        }
    }
}

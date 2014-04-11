using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HipperChat.Core.Helpers
{
    internal static class HttpHelpers
    {
        public static string PostObject(string url, object postObject)
        {
            var postData = JsonConvert.SerializeObject(postObject, Formatting.Indented, new JsonSerializerSettings
            {
                ContractResolver = new LowercaseJsonResolver(),
                NullValueHandling = NullValueHandling.Ignore
            });
            var byteData = Encoding.UTF8.GetBytes(postData);

            var request = HttpWebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = byteData.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(byteData, 0, byteData.Length);
                stream.Close();
            }

            var reader = new StreamReader(request.GetResponse().GetResponseStream());
            var response = reader.ReadToEnd();
            return response;
        }

        private class LowercaseJsonResolver : DefaultContractResolver
        {
            protected override string ResolvePropertyName(string propertyName)
            {
                return base.ResolvePropertyName(propertyName).ToLowerInvariant();
            }
        }
    }
}

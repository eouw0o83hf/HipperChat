using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HipperChat.Core
{
    public abstract class BaseService
    {
        private const string HipchatServerUrl = @"https://api.hipchat.com/v2/";

        private static string BuildUrl(string command, string token, IDictionary<string, string> urlParams = null)
        {
            var builder = new StringBuilder();

            builder.Append(HipchatServerUrl);
            builder.Append(command);

            var paramCount = 0;
            if (urlParams != null)
            {
                foreach (var param in urlParams)
                {
                    builder
                        .Append(paramCount++ == 0 ? "?" : "&")
                        .Append(param.Key)
                        .Append("=")
                        .Append(param.Value);
                }
            }

            if (!string.IsNullOrWhiteSpace(token))
            {
                builder
                    .Append(paramCount++ == 0 ? "?" : "&")
                    .Append("auth_token")
                    .Append("=")
                    .Append(token);
            }

            return builder.ToString();
        }
        
        protected static T GetActionResult<T>(string command, string token, IDictionary<string, string> urlParams = null)
        {
            var url = BuildUrl(command, token, urlParams);

            string json;
            using (var client = new WebClient())
            {
                json = client.DownloadString(url);
            }

            return JsonConvert.DeserializeObject<T>(json);
        }
        
        protected static string PostObject(string command, string token, object postObject, IDictionary<string, string> urlParams = null)
        {
            var postData = JsonConvert.SerializeObject(postObject, Formatting.Indented, new JsonSerializerSettings
            {
                ContractResolver = new LowercaseJsonResolver(),
                NullValueHandling = NullValueHandling.Ignore
            });
            var byteData = Encoding.UTF8.GetBytes(postData);

            var url = BuildUrl(command, token, urlParams);
            var request = HttpWebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = byteData.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(byteData, 0, byteData.Length);
                stream.Close();
            }

            string response;
            using (var reader = new StreamReader(request.GetResponse().GetResponseStream()))
            {
                response = reader.ReadToEnd();
                reader.Close();
            }

            return response;
        }

        protected static T PostObject<T>(string command, string token, object postObject, IDictionary<string, string> urlParams = null)
        {
            var response = PostObject(command, token, postObject, urlParams);
            return JsonConvert.DeserializeObject<T>(response);
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

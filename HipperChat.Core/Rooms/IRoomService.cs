using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using HipperChat.Core.Json;

namespace HipperChat.Core.Rooms
{
    public interface IRoomService
    {
        ICollection<Room> GetRooms();
        void SendMessage(int roomId, Message message);
    }

    public class RoomService : IRoomService
    {
        private readonly string _apiKey;

        public RoomService(string apiKey)
        {
            _apiKey = apiKey;
        }

        public ICollection<Room> GetRooms()
        {
            var client = new WebClient();
            var json = client.DownloadString("https://api.hipchat.com/v2/room?auth_token=" + _apiKey);
            var response = JsonConvert.DeserializeObject<GenericResult<Room>>(json);
            return response.Items;
        }

        public void SendMessage(int roomId, Message message)
        {
            var postData = JsonConvert.SerializeObject(message, Formatting.Indented, new JsonSerializerSettings
            {
                ContractResolver = new LowercaseJsonResolver(),
                NullValueHandling = NullValueHandling.Ignore
            });
            var byteData = Encoding.UTF8.GetBytes(postData);

            var request = HttpWebRequest.Create("https://api.hipchat.com/v2/room/" + roomId + "/notification?auth_token=" + _apiKey);
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
            System.Console.WriteLine(response);
        }
    }
}

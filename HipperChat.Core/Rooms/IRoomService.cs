using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using HipperChat.Core.Helpers;

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
            var response = HttpHelpers.PostObject("https://api.hipchat.com/v2/room/" + roomId + "/notification?auth_token=" + _apiKey, message);
        }
    }
}

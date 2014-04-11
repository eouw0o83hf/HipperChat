using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HipperChat.Core.Rooms
{
    public interface IRoomService
    {
        ICollection<Room> GetRooms();
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
    }
}

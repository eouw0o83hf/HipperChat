using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HipperChat.Core.Rooms
{
    public interface IHipChatRoomService
    {
        ICollection<Room> GetRooms();
    }

    public class HipChatRoomService : IHipChatRoomService
    {
        private readonly string _apiKey;

        public HipChatRoomService(string apiKey)
        {
            _apiKey = apiKey;
        }

        public ICollection<Room> GetRooms()
        {
            var client = new WebClient();
            var json = client.DownloadString("https://api.hipchat.com/v2/room?auth_token=" + _apiKey);
            var response = JsonConvert.DeserializeObject<GetRoomsResult>(json);
            return response.Items;
        }

        private class GetRoomsResult
        {
            public ICollection<Room> Items { get; set; }
            public int StartIndex { get; set; }
            public int MaxResults { get; set; }
            public Links Links { get; set; }
        }
    }
}

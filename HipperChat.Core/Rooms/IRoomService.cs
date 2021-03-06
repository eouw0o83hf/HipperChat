﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace HipperChat.Core.Rooms
{
    public interface IRoomService
    {
        ICollection<Room> GetRooms(string apiKey);
        void SendMessage(string apiKey, int roomId, Message message);
        ICollection<IncomingChat> GetHistory(string apiKey, int roomId);
    }

    public class RoomService : BaseService, IRoomService
    {
        public ICollection<Room> GetRooms(string apiKey)
        {
            return DoGet<GenericResult<Room>>("room", apiKey).Items;
        }

        public void SendMessage(string apiKey, int roomId, Message message)
        {
            var command = string.Format("room/{0}/notification", roomId);
            var response = DoPost(command, apiKey, message);
        }

        public ICollection<IncomingChat> GetHistory(string apiKey, int roomId)
        {
            var command = string.Format("room/{0}/history", roomId);
            var response = DoGet<GenericResult<IncomingChat>>(command, apiKey);
            return response.Items;
        }
    }
}

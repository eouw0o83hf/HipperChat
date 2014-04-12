using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HipperChat.Web.Models.Chat
{
    public class SelectRoomViewModel
    {
        public IDictionary<int, string> Rooms { get; set; }
    }
}
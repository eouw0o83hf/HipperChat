using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HipperChat.Web.Models.Chat
{
    public class RoomViewModel
    {
        public ICollection<IncomingMessage> Messages { get; set; }
    }

    public class IncomingMessage
    {
        public string From { get; set; }
        public string Color { get; set; }
        public string Message { get; set; }
    }
}
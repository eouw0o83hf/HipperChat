using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HipperChat.Web.Models.WriteOnly
{
    public class MessageModel
    {
        [AllowHtml]
        public string Message { get; set; }
        public bool IsHtml { get; set; }
        public bool SuchAnnoy { get; set; }
        public string Color { get; set; }
        public string ApiKey { get; set; }

        public List<RoomItem> Rooms { get; set; }
    }

    public class RoomItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}
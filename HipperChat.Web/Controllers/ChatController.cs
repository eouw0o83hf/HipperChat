using HipperChat.Core.Rooms;
using HipperChat.Web.Models.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HipperChat.Web.Controllers
{
    public class ChatController : Controller
    {
        public ActionResult Index()
        {
            if (ApiKey != null)
            {
                return RedirectToAction("Room");
            }

            return View();
        }

        public ActionResult ChooseRoom(string apiKey)
        {
            ApiKey = apiKey;

            var roomService = new RoomService();
            var rooms = roomService.GetRooms(Session["ApiKey"].ToString());

            return View(new SelectRoomViewModel
            {
                Rooms = rooms.ToDictionary(a => a.Id, a => a.Name)
            });
        }

        public ActionResult Room(int roomId)
        {
            var roomService = new RoomService();
            var history = roomService.GetHistory(ApiKey, roomId);
            var model = new RoomViewModel
            {
                Messages = history.Select(a => new IncomingMessage
                {
                    Color = a.Color,
                    From = a.From.Name,
                    Message = a.Message
                }).ToList()
            };
            return View(model);
        }

        private string ApiKey
        {
            get
            {
                return Session["ApiKey"] as string;
            }
            set
            {
                Session["ApiKey"] = value;
            }
        }
    }
}

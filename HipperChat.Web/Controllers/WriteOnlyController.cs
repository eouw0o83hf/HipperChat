using HipperChat.Core.Emoticons;
using HipperChat.Core.Helpers;
using HipperChat.Core.Rooms;
using HipperChat.Web.Models.WriteOnly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HipperChat.Web.Controllers
{
    public class WriteOnlyController : Controller
    {        
        [HttpGet]
        public ActionResult Index(string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                return View("NoApiKey");
            }

            // Yup, I'm new()ing things up.
            var roomService = new RoomService(apiKey);
            var rooms = roomService.GetRooms();

            var emoticonService = new EmoticonService(apiKey);
            var emoticons = emoticonService.GetEmoticons();

            var model = new MessageModel
            {
                ApiKey = apiKey,
                Color = "random",
                SuchAnnoy = false,
                IsHtml = false,
                Rooms = rooms.Select(a => new RoomItem
                {
                    Id = a.Id,
                    IsSelected = false,
                    Name = a.Name
                }).ToList(),
                Emoticons = emoticons.Select(a => new EmoticonItem
                {
                    Code = a.Shortcut,
                    IsGlobal = a.Source == EmoticonSource.Global,
                    Url = a.Url
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(MessageModel model)
        {
            var roomService = new RoomService(model.ApiKey);
            var roomId = model.Rooms.Where(a => a.IsSelected).Select(a => (int?)a.Id).FirstOrDefault();
            if (roomId == null)
            {
                return new EmptyResult();
            }

            var message = new Message
            {
                Color = model.Color,
                MessageFormat = model.IsHtml ? MessageFormatEnum.Html : MessageFormatEnum.Text,
                Notify = model.SuchAnnoy,
                Text = model.Message
            };
            roomService.SendMessage(roomId.Value, message);


            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult Dogify(string toDogify)
        {
            if (String.IsNullOrEmpty(toDogify))
            {
                return new EmptyResult();
            }
            var dogifier = new DogifyService();
            var result = dogifier.Dogify(toDogify);
            return new ContentResult{Content = result.ConvenientlyFormattedDogePairs };
        }
    }
}

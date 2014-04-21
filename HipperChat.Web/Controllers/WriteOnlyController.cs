using HipperChat.Core.Emoticons;
using HipperChat.Core.Helpers;
using HipperChat.Core.Rooms;
using HipperChat.Core.Users;
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
            var roomService = new RoomService();
            var rooms = roomService.GetRooms(apiKey);

            var userService = new UserService();
            var users = userService.GetUsers(apiKey).Where(a => !a.IsDeleted);

            var emoticonService = new EmoticonService();
            var emoticons = emoticonService.GetEmoticons(apiKey);

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
                }).ToList(),
                Users = users.Select(a => new UserItem
                    {
                        Id = a.Id,
                        Name = a.Name,
                        MentionName = a.MentionName
                    }).ToList()
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult Read(string apiKey, int roomId)
        {
            var roomService = new RoomService();
            var history = roomService.GetHistory(apiKey, roomId);
            var model = new ReadModel { Items = history.Select(a => a.Message).ToList() };
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(MessageModel model)
        {
            var roomService = new RoomService();
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
            roomService.SendMessage(model.ApiKey, roomId.Value, message);


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
            return new ContentResult { Content = result.ConvenientlyFormattedDogePairs };
        }

        [HttpPost]
        public ActionResult DogifyImg(string toDogify)
        {
            if (String.IsNullOrEmpty(toDogify))
            {
                return new EmptyResult();
            }
            var dogifier = new DogifyService();
            var result = dogifier.Dogify(toDogify);
            return new ContentResult { Content = result.DaasImgUrl.ToString() };
        }
    }
}

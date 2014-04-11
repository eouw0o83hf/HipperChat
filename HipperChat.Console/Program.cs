using HipperChat.Core;
using HipperChat.Core.Emoticons;
using HipperChat.Core.Rooms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HipperChat.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var apiKey = File.ReadAllText(@"C:\hipchatapikey.txt");

            IRoomService core = new RoomService(apiKey);
            IEmoticonService emo = new EmoticonService(apiKey);

            var single = emo.GetEmoticon("un");

            var emoticons = emo.GetEmoticons();

            foreach (var emote in emoticons)
            {
                System.Console.WriteLine("{0}: ({1}) {2}, {3}", emote.Id, emote.Shortcut, emote.Url, emote.Links.Self);
            }

            var rooms = core.GetRooms();

            foreach (var room in rooms)
            {
                System.Console.WriteLine("{0}: {1} ({2})", room.Id, room.Name, room.Links.Self);
            }

            System.Console.WriteLine("Done");
            System.Console.Read();
        }
    }
}

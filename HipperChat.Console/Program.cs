using HipperChat.Core;
using HipperChat.Core.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HipperChat.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHipChatRoomService core = new HipChatRoomService("");

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

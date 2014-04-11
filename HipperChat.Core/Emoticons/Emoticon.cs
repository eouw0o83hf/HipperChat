using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HipperChat.Core.Emoticons
{
    public class Emoticon
    {
        public int Id { get; set; }
        public string Shortcut { get; set; }

        public EmoticonSource Source { get; set; }

        public string Url { get; set; }
        public Links Links { get; set; }
    }

    public enum EmoticonSource
    {
        Global,
        Group
    }
}

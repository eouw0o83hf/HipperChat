using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HipperChat.Core.Emoticons
{
    public class EmoticonMetadata
    {
        public int Id { get; set; }
        public string Shortcut { get; set; }

        public int Height { get; set; }
        public int Width { get; set; }

        public string AudioPath { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HipperChat.Web.Models.WriteOnly
{
    public class ReadModel
    {
        public ICollection<string> Items { get; set; }
    }
}
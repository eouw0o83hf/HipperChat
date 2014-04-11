using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HipperChat.Core.Users
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string MentionName { get; set; }
        public bool IsDeleted { get; set; }
    }
}

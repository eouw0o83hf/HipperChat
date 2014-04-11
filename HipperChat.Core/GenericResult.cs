using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HipperChat.Core
{
    public class GenericResult<T>
    {
        public ICollection<T> Items { get; set; }

        public int StartIndex { get; set; }
        public int MaxResults { get; set; }
        public Links Links { get; set; }
    }
}

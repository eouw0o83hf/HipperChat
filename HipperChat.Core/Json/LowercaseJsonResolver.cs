using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HipperChat.Core.Json
{
    internal class LowercaseJsonResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return base.ResolvePropertyName(propertyName).ToLowerInvariant();
        }
    }
}

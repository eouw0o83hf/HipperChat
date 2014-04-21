using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HipperChat.Core.Helpers
{
    public class DogeResult
    {
        public string OriginalString { get; set; }

        public string ConvenientlyFormattedDogePairs { get; set; }

        public List<DogePair> DogePairs { get; set; }

        public Uri DaasImgUrl { get; set; }
    }

    public class DogePair
    {
        public DogeAccent Accent { get; set; }

        public string AccentBase { get; set; }

        public override string ToString()
        {
            return String.Format("{0}{1}", Accent.ToString().ToLower(), Accent == DogeAccent.Wow ? String.Empty : " " + AccentBase.ToLower());
        }

        public static DogePair Wow
        {
            get
            {
                return new DogePair
                {
                    Accent = DogeAccent.Wow
                };
            }
        }
    }

    public enum DogeAccent
    {
        Wow,
        Very,
        Much,
        Many,
        Such
    }

}

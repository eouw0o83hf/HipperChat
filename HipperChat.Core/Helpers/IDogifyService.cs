using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HipperChat.Core.Helpers
{
    public interface IDogifyService
    {
        DogeResult Dogify(string fullString, int? length = 0);
        DogePair DogifySingle(string word);
        string Format(List<DogePair> dogePairs, int length);
    }

    public class DogifyService : IDogifyService
    {
        private readonly List<string> _articles = new List<string>
            {
                "the",
                "a",
                "an",
                "some",
                "no",
            };

        private Random _rng = new Random();

        public DogeResult Dogify(string fullString, int? length = 0)
        {
            var fullList = Regex.Split(fullString, @"[^\w]").Select(a => a.ToLower()).Where(a => a != String.Empty && !_articles.Contains(a)).ToList();

            var result = new DogeResult
                {
                    OriginalString = fullString,
                    DogePairs = new List<DogePair> { DogePair.Wow }
                };

            int max = 0;
            foreach (var s in fullList)
            {
                DogeAccent dummy;
                if (DogeAccent.TryParse(s, out dummy))
                {
                    continue;
                }

                if (_rng.NextDouble() > 0.2) // for determinism
                {
                    result.DogePairs.Add(DogePair.Wow);
                }

                var newPair = DogifySingle(s);
                result.DogePairs.Add(newPair);
                max = Math.Max(max, newPair.ToString().Length);
            }

            result.ConvenientlyFormattedDogePairs = Format(result.DogePairs, max*2);

            return result;
        }

        private readonly List<DogeAccent> _possibleAccents =
            Enum.GetValues(typeof (DogeAccent)).OfType<DogeAccent>().Where(a => a != DogeAccent.Wow).ToList();

        public DogePair DogifySingle(string word)
        {
            if (String.IsNullOrEmpty(word))
            {
                throw new ArgumentException("Cannot be null or empty", "word");
            }

            var newIndex = _rng.Next(_possibleAccents.Count);

            return new DogePair
                {
                    Accent = _possibleAccents[newIndex],
                    AccentBase = word
                };
        }

        public string Format(List<DogePair> dogePairs, int length)
        {
            if (length < dogePairs.Max(a => a.ToString().Length))
            {
                throw new ArgumentException("Length is shorter than a dogepair", "length");
            }
            var builder = new StringBuilder();

            foreach (var dp in dogePairs)
            {
                var pairString = dp.ToString();

                var playLength = length - pairString.Length;

                if (playLength == 0)
                {
                    builder.AppendLine(pairString);
                    continue;
                }

                var indent = 0;
                if (_rng.NextDouble() > 0.5)
                {
                    indent = _rng.Next(playLength + 1);
                }

                builder.AppendFormat("{0}{1}{2}\n", new String(' ', indent), pairString,
                                     new String(' ', playLength - indent));
            }

            return builder.ToString();
        }
    }
}

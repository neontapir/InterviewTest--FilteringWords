using System;
using System.Collections.Generic;
using System.Linq;
using Interview.FilteringWords.Domain;

namespace Interview.FilteringWords.Tests
{
    public class MyFilter : IFilter
    {
        private readonly int _stringLength;

        public MyFilter(int stringLength)
        {
            if (stringLength == 0)
                throw new ArgumentOutOfRangeException("stringLength", "Can't be zero");
            if (stringLength < 0)
                throw new ArgumentOutOfRangeException("stringLength", "Can't be below zero");

            _stringLength = stringLength;
        }

        public IEnumerable<string> Apply1(IEnumerable<string> input)
        {
            var words = input as string[] ?? new string[0];

            var fragmentHash = new Dictionary<string, int>();
            var rightLengthWords = words.Where(x => x.Length == _stringLength).ToArray();

            var fragments = words.Where(x => x.Length < _stringLength);
            foreach (var word in fragments.SelectMany(f => rightLengthWords.Where(word => word.Contains(f))))
            {
                fragmentHash[word] = fragmentHash.ContainsKey(word) ? fragmentHash[word] + 1 : 1;
            }

            return fragmentHash.Where(fh => fh.Value > 1 && rightLengthWords.Contains(fh.Key)).Select(m => m.Key);
        }

        public IEnumerable<string> Apply(IEnumerable<string> input)
        {
            var words = input as string[] ?? new string[0];

            var fragmentHash = new Dictionary<string, IEnumerable<string>>();
            var rightLengthWords = words.Where(x => x.Length == _stringLength).ToArray();
            var fragments = words.Where(x => x.Length < _stringLength).ToArray();

            foreach (var aWord in rightLengthWords)
            {
                string word = aWord; // keep closure
                fragmentHash[word] = new string[0];
                foreach (var fragment in fragments.Where(word.Contains))
                {
                    fragmentHash[word] = fragmentHash[word].Concat(new[] { fragment });
                }
            }
            return fragmentHash.Where(fh => rightLengthWords.Contains(fh.Key) && fh.Key.Length == fh.Value.Sum(x => x.Length))
                .Select(m => m.Key);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace Interview.FilteringWords.Domain
{

    /*
     * Name: Leonardo Fernandez Sanchez (leonfs@gmail.com)
     * 
     * The good thing about the solution is that implements a really simple algorithm. There is no complex data structures to understand. Also the filter allows multiple lengths. 
     * 
     * What could be improved is corollary to the good thing. The simple algorithm has a complexity of O(n2) - cuadratic - making it not perform well for big N. 
     * With the use of Trie Data structure for the smaller strings an improvement of the algorithm complexity could have been made.
     * 
     * Things that I haven't considered are the use of Capital Letters on the strings, and if the resulting list could contain repeated elements.
     * 
     */
    public class Filter : IFilter
    {
        private readonly int _stringLength;

        public Filter(int stringLength)
        {
            if (stringLength <= 0)
                throw new ArgumentOutOfRangeException("stringLength", "Length has to be greater than zero.");

            _stringLength = stringLength;
        }

        public IEnumerable<string> Apply(IEnumerable<string> stringsToFilter)
        {
            if (stringsToFilter == null)
                return new List<string>();
            
            var composableStrings = new HashSet<string>();
            foreach (var stringToFilter in stringsToFilter.Where(stringToFilter => stringToFilter.Length < _stringLength))
            {
                composableStrings.Add(stringToFilter);
            }

            var filteredStrings = new List<string>();
            foreach (string stringToFilter in stringsToFilter) 
            {
                if (stringToFilter.Length != _stringLength)
                    continue;

                foreach (string startingSmallString in composableStrings) 
                {
                    if (stringToFilter.StartsWith(startingSmallString))
                    {
                        var stringToFilterEnding = stringToFilter.Remove(0, startingSmallString.Length);

                        if (composableStrings.Contains(stringToFilterEnding))
                            filteredStrings.Add(stringToFilter);
                    }
                }
            }

            return filteredStrings;
        }
    }
}
using System.Collections.Generic;

namespace Interview.FilteringWords.Domain
{
    public interface IFilter
    {
        IEnumerable<string> Apply(IEnumerable<string> stringsToFilter);
    }
}
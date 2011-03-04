using System.Collections.Generic;
using FacetedSearch.Params;

namespace FacetedSearch
{
    public class SearchOptions : ISearchOptions
    {
        private IList<ISearchOptionsParam> _params = new List<ISearchOptionsParam>();
    }
}
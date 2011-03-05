using System.Collections.Generic;
using FacetedSearch.Params;

namespace FacetedSearch
{
    public class SearchOptions : ISearchOptions
    {
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IList<ISearchOptionsParam> _params = new List<ISearchOptionsParam>();

        public SearchOptions(IJsonSerializer jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
        }

        public string GetJson()
        {
            var paramsPart = _jsonSerializer.Serialize(_params);

            //add extra options to json, think about versioning

            return paramsPart;
        }
    }
}
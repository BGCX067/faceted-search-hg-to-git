using FacetedSearch.Params;

namespace FacetedSearch.Builder
{
    public class SearchOptionsBuilder
    {
        private readonly SearchOptions _searchOptions;

        public SearchOptionsBuilder(IJsonSerializer jsonSerializer = null)
        {
            jsonSerializer = jsonSerializer ?? new DefaultJsonSerializer();
            _searchOptions = new SearchOptions(jsonSerializer);
        }

        public TextSearchOptionsParamBuilder Text(string searchOptionsName)
        {
            var textSearchOptionsParam = new TextSearchOptionsParam(searchOptionsName);
            _searchOptions.AddParam(textSearchOptionsParam);
            return new TextSearchOptionsParamBuilder(textSearchOptionsParam, this);
        }

        public SearchOptions BuildSearchOptions()
        {
            //perform additional manipultation
            return _searchOptions;
        }
    }
}
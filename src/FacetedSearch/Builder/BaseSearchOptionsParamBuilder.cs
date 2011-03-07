using FacetedSearch.Params;

namespace FacetedSearch.Builder
{
    public abstract class BaseSearchOptionsParamBuilder<T> where T : BaseSearchOptionsParam
    {
        protected readonly SearchOptionsBuilder _searchOptionsBuilder;
        protected T _param;

        protected BaseSearchOptionsParamBuilder(T param, SearchOptionsBuilder searchOptionsBuilder)
        {
            _param = param;
            _searchOptionsBuilder = searchOptionsBuilder;
        }

        public SearchOptionsBuilder End()
        {
            return _searchOptionsBuilder;
        }
    }
}
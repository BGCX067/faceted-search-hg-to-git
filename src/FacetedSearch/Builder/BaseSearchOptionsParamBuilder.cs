using FacetedSearch.Params;

namespace FacetedSearch.Builder
{
    public abstract class BaseSearchOptionsParamBuilder<T, TBuilder>
        where T : BaseSearchOptionsParam
        where TBuilder : BaseSearchOptionsParamBuilder<T, TBuilder>
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

        public TBuilder Description(string description)
        {
            _param.Description = description;
            return (TBuilder) this;
        }
    }
}
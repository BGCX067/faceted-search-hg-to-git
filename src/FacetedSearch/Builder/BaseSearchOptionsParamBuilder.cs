using FacetedSearch.Mapping;
using FacetedSearch.Params;

namespace FacetedSearch.Builder
{
    public abstract class BaseSearchOptionsParamBuilder<T, TBuilder, TModel>
        where T : BaseSearchOptionsParam
        where TBuilder : BaseSearchOptionsParamBuilder<T, TBuilder, TModel> 
        where TModel : new()
    {
        protected readonly SearchOptionsBuilder<TModel> _searchOptionsBuilder;
        protected readonly FacatedSearchMapper<TModel> _queryMapper;
        protected T _param;

        protected BaseSearchOptionsParamBuilder(T param, SearchOptionsBuilder<TModel> searchOptionsBuilder, FacatedSearchMapper<TModel> queryMapper)
        {
            _param = param;
            _queryMapper = queryMapper;
            _searchOptionsBuilder = searchOptionsBuilder;
        }

        public SearchOptionsBuilder<TModel> End()
        {
            return _searchOptionsBuilder;
        }

        public TBuilder Description(string description)
        {
            _param.Description = description;
            return (TBuilder) this;
        }
        
        public TBuilder Order(int orderIndex)
        {
            _param.Order = orderIndex;
            return (TBuilder) this;
        }
        
        public TBuilder Help(string help)
        {
            _param.Help = help;
            return (TBuilder) this;
        }
    }
}
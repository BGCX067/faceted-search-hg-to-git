using FacetedSearch.Builder;
using FacetedSearch.Mapping;
using FacetedSearch.Params;

namespace FacetedSearch.Factory
{
    public class SearchOptionsQueryParamBuilderBuilderFactory<TModel> : ISearchOptionsParamBuilderFactory
        where TModel : new()
    {
        private readonly FacatedSearchMapper<TModel> _queryMapper;

        public SearchOptionsQueryParamBuilderBuilderFactory(FacatedSearchMapper<TModel> queryMapper)
        {
            _queryMapper = queryMapper;
        }

        #region ISearchOptionsParamBuilderFactory Members

        public TextSearchOptionsParamBuilder GetTextParamBuilder(TextSearchOptionsParam textSearchOptionsParam,
                                                                 SearchOptionsBuilder searchOptionsBuilder)
        {
            return new TextSearchOptionsParamBuilder<TModel>(textSearchOptionsParam, searchOptionsBuilder, _queryMapper);
        }

        public CheckboxSearchOptionsParamBuilder GetCheckboxParamBuilder(
            CheckboxSearchOptionsParam checkboxSearchOptionsParam, SearchOptionsBuilder searchOptionsBuilder)
        {
            return new CheckboxSearchOptionsParamBuilder<TModel>(checkboxSearchOptionsParam, searchOptionsBuilder,
                                                                 _queryMapper);
        }

        #endregion
    }
}
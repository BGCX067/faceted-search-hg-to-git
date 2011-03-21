using FacetedSearch.Builder;
using FacetedSearch.Mapping;
using FacetedSearch.Params;

namespace FacetedSearch.Factory
{
    public class SearchOptionsQueryParamBuilderBuilderFactory<TModel> : ISearchOptionsParamBuilderFactory<TModel>
        where TModel : new()
    {
        private readonly FacatedSearchMapper<TModel> _queryMapper;

        public SearchOptionsQueryParamBuilderBuilderFactory(FacatedSearchMapper<TModel> queryMapper)
        {
            _queryMapper = queryMapper;
        }

        #region ISearchOptionsParamBuilderFactory Members

        public TextSearchOptionsParamBuilder<TModel> GetTextParamBuilder(TextSearchOptionsParam textSearchOptionsParam,
                                                                 SearchOptionsBuilder<TModel> searchOptionsBuilder)
        {
            return new TextSearchOptionsParamBuilder<TModel>(textSearchOptionsParam, searchOptionsBuilder, _queryMapper);
        }

        public CheckboxSearchOptionsParamBuilder<TModel> GetCheckboxParamBuilder(
            CheckboxSearchOptionsParam checkboxSearchOptionsParam, SearchOptionsBuilder<TModel> searchOptionsBuilder)
        {
            return new CheckboxSearchOptionsParamBuilder<TModel>(checkboxSearchOptionsParam, searchOptionsBuilder,
                                                                 _queryMapper);
        }

        #endregion
    }
}
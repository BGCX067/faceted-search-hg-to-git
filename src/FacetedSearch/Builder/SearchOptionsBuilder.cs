namespace FacetedSearch.Builder
{
    using System;
    using Factory;
    using Lokad;
    using Mapping;
    using Params;

    public class SearchOptionsBuilder<TModel> where TModel : new()
    {
        private readonly FacatedSearchMapper<TModel> _queryMapper = new FacatedSearchMapper<TModel>();

        private readonly SearchOptions<TModel> _searchOptions;

        protected ISearchOptionsParamBuilderFactory<TModel> _searchOptionsParamBuilderBuilderFactory;

        public SearchOptionsBuilder()
        {
            _searchOptions = new SearchOptions<TModel> {QueryMapper = _queryMapper};
            _searchOptionsParamBuilderBuilderFactory =
                new SearchOptionsQueryParamBuilderBuilderFactory<TModel>(_queryMapper);
        }

        public TextSearchOptionsParamBuilder<TModel> Text(string searchOptionsName = "")
        {
            var textSearchOptionsParam = new TextSearchOptionsParam(searchOptionsName);
            _searchOptions.AddParam(textSearchOptionsParam);
            return _searchOptionsParamBuilderBuilderFactory.GetTextParamBuilder(textSearchOptionsParam, this);
        }

        public CheckboxSearchOptionsParamBuilder<TModel> Checkbox(string searchOptionsName = "")
        {
            var checkboxSearchOptionsParam = new CheckboxSearchOptionsParam(searchOptionsName);
            _searchOptions.AddParam(checkboxSearchOptionsParam);
            return _searchOptionsParamBuilderBuilderFactory.GetCheckboxParamBuilder(checkboxSearchOptionsParam, this);
        }

        public SearchOptionsBuilder<TModel> Text(Action<TextSearchOptionsParamBuilder<TModel>> action)
        {
            Enforce.Argument(() => action);

            action(Text());
            return this;
        }

        public SearchOptionsBuilder<TModel> Checkbox(Action<CheckboxSearchOptionsParamBuilder<TModel>> action)
        {
            Enforce.Argument(() => action);

            action(Checkbox());
            return this;
        }

        public SearchOptions<TModel> BuildSearchOptions()
        {
            //perform additional manipultation
            return _searchOptions;
        }
    }
}
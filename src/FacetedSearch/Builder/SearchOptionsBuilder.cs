using System;
using FacetedSearch.Factory;
using FacetedSearch.Mapping;
using FacetedSearch.Params;
using Lokad;

namespace FacetedSearch.Builder
{
    public class SearchOptionsBuilder<TModel> : SearchOptionsBuilder where TModel : new()
    {
        private readonly FacatedSearchMapper<TModel> _queryMapper = FacatedSearch.Map<TModel>();

        protected override ISearchOptionsParamBuilderFactory SearchOptionsParamBuilderBuilderFactory
        {
            get
            {
                return _searchOptionsParamBuilderBuilderFactory ??
                       (_searchOptionsParamBuilderBuilderFactory = new SearchOptionsQueryParamBuilderBuilderFactory<TModel>(_queryMapper));
            }
        }

        public new TextSearchOptionsParamBuilder<TModel> Text(string searchOptionsName = "")
        {
            return (TextSearchOptionsParamBuilder<TModel>) base.Text(searchOptionsName);
        }

        public new CheckboxSearchOptionsParamBuilder<TModel> Checkbox(string searchOptionsName = "")
        {
            return (CheckboxSearchOptionsParamBuilder<TModel>)base.Checkbox(searchOptionsName);
        }
    }

    public class SearchOptionsBuilder
    {
        private readonly SearchOptions _searchOptions;

        protected ISearchOptionsParamBuilderFactory _searchOptionsParamBuilderBuilderFactory;

        public SearchOptionsBuilder(IJsonSerializer jsonSerializer = null)
        {
            jsonSerializer = jsonSerializer ?? new DefaultJsonSerializer();
            _searchOptions = new SearchOptions(jsonSerializer);
        }

        protected virtual ISearchOptionsParamBuilderFactory SearchOptionsParamBuilderBuilderFactory
        {
            get { return _searchOptionsParamBuilderBuilderFactory ?? (_searchOptionsParamBuilderBuilderFactory = new SearchOptionsParamBuilderBuilderFactory()); }
        }

        public TextSearchOptionsParamBuilder Text(string searchOptionsName = "")
        {
            var textSearchOptionsParam = new TextSearchOptionsParam(searchOptionsName);
            _searchOptions.AddParam(textSearchOptionsParam);
            return _searchOptionsParamBuilderBuilderFactory.GetTextParamBuilder(textSearchOptionsParam, this);
        }

        public CheckboxSearchOptionsParamBuilder Checkbox(string searchOptionsName = "")
        {
            var checkboxSearchOptionsParam = new CheckboxSearchOptionsParam(searchOptionsName);
            _searchOptions.AddParam(checkboxSearchOptionsParam);
            return _searchOptionsParamBuilderBuilderFactory.GetCheckboxParamBuilder(checkboxSearchOptionsParam, this);
        }

        public SearchOptionsBuilder Text(Action<TextSearchOptionsParamBuilder> action)
        {
            Enforce.Argument(() => action);

            action(Text());
            return this;
        }

        public SearchOptionsBuilder Checkbox(Action<CheckboxSearchOptionsParamBuilder> action)
        {
            Enforce.Argument(() => action);

            action(Checkbox());
            return this;
        }

        public SearchOptions BuildSearchOptions()
        {
            //perform additional manipultation
            return _searchOptions;
        }
    }
}
using System;
using FacetedSearch.Factory;
using FacetedSearch.Mapping;
using FacetedSearch.Params;
using Lokad;

namespace FacetedSearch.Builder
{
    public class SearchOptionsBuilder<TModel> where TModel : new()
    {
        private readonly FacatedSearchMapper<TModel> _queryMapper = FacatedSearch.Map<TModel>();

        protected ISearchOptionsParamBuilderFactory<TModel> SearchOptionsParamBuilderBuilderFactory
        {
            get
            {
                return _searchOptionsParamBuilderBuilderFactory ??
                       (_searchOptionsParamBuilderBuilderFactory = new SearchOptionsQueryParamBuilderBuilderFactory<TModel>(_queryMapper));
            }
        }

        private readonly SearchOptions _searchOptions;

        protected ISearchOptionsParamBuilderFactory<TModel> _searchOptionsParamBuilderBuilderFactory;

        public SearchOptionsBuilder(IJsonSerializer jsonSerializer = null)
        {
            jsonSerializer = jsonSerializer ?? new DefaultJsonSerializer();
            _searchOptions = new SearchOptions(jsonSerializer);
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

        public SearchOptions BuildSearchOptions()
        {
            //perform additional manipultation
            return _searchOptions;
        }
    }
}
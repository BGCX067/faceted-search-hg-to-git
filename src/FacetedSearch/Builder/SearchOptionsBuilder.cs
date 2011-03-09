using System;
using FacetedSearch.Params;
using Lokad;

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

        public TextSearchOptionsParamBuilder Text(string searchOptionsName = "")
        {
            var textSearchOptionsParam = new TextSearchOptionsParam(searchOptionsName);
            _searchOptions.AddParam(textSearchOptionsParam);
            return new TextSearchOptionsParamBuilder(textSearchOptionsParam, this);
        }

        public CheckboxSearchOptionsParamBuilder Checkbox(string searchOptionsName = "")
        {
            var checkboxSearchOptionsParam = new CheckboxSearchOptionsParam(searchOptionsName);
            _searchOptions.AddParam(checkboxSearchOptionsParam);
            return new CheckboxSearchOptionsParamBuilder(checkboxSearchOptionsParam, this);
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
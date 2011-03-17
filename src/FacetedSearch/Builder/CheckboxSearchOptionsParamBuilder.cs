using System;
using FacetedSearch.Mapping;
using FacetedSearch.Params;
using Lokad;

namespace FacetedSearch.Builder
{
    public class CheckboxSearchOptionsParamBuilder<TModel> : CheckboxSearchOptionsParamBuilder where TModel : new()
    {
        private readonly FacatedSearchMapper<TModel> _queryMapper;

        public CheckboxSearchOptionsParamBuilder
            (CheckboxSearchOptionsParam param, SearchOptionsBuilder searchOptionsBuilder,
             FacatedSearchMapper<TModel> queryMapper)
            : base(param, searchOptionsBuilder)
        {
            _queryMapper = queryMapper;
        }

        public CheckboxSearchOptionsParamBuilder<TModel> MapQuery(Action<FacatedSearchMapper<TModel>> action)
        {
            Enforce.Argument(() => action);

            action(_queryMapper);
            return this;
        }
    }


    public class CheckboxSearchOptionsParamBuilder :
        BaseSearchOptionsParamBuilder<CheckboxSearchOptionsParam, CheckboxSearchOptionsParamBuilder>
    {
        public CheckboxSearchOptionsParamBuilder(CheckboxSearchOptionsParam param,
                                                 SearchOptionsBuilder searchOptionsBuilder)
            : base(param, searchOptionsBuilder)
        {
            _param = param;
        }

        public CheckboxSearchOptionsParamBuilder Disabled(bool isDisabled = true)
        {
            _param.IsDisabled = isDisabled;
            return this;
        }

        public CheckboxSearchOptionsParamBuilder Checked(bool isChecked = true)
        {
            _param.IsChecked = isChecked;
            return this;
        }
    }
}
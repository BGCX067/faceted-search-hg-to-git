using System;
using System.Linq.Expressions;
using FacetedSearch.Mapping;
using FacetedSearch.Params;
using Lokad;

namespace FacetedSearch.Builder
{
    public class CheckboxSearchOptionsParamBuilder<TModel> : BaseSearchOptionsParamBuilder<CheckboxSearchOptionsParam, CheckboxSearchOptionsParamBuilder<TModel>, TModel> where TModel : new()
    {
        public CheckboxSearchOptionsParamBuilder
            (CheckboxSearchOptionsParam param, SearchOptionsBuilder<TModel> searchOptionsBuilder,
             FacatedSearchMapper<TModel> queryMapper)
            : base(param, searchOptionsBuilder, queryMapper)
        {
        }

        public CheckboxSearchOptionsParamBuilder<TModel> MapQuery<TProperty>(Expression<Func<TModel, TProperty>> property)
        {
            Enforce.Argument(() => property);

            _queryMapper.Property(property, _param.Name);
            return this;
        }

        public CheckboxSearchOptionsParamBuilder<TModel> Disabled(bool isDisabled = true)
        {
            _param.IsDisabled = isDisabled;
            return this;
        }

        public CheckboxSearchOptionsParamBuilder<TModel> Checked(bool isChecked = true)
        {
            _param.IsChecked = isChecked;
            return this;
        }
    }
}
using System;
using System.Linq.Expressions;
using FacetedSearch.Mapping;
using FacetedSearch.Params;
using Lokad;

namespace FacetedSearch.Builder
{
    public class TextSearchOptionsParamBuilder<TModel> : BaseSearchOptionsParamBuilder<TextSearchOptionsParam, TextSearchOptionsParamBuilder<TModel>, TModel> where TModel : new()
    {
        public TextSearchOptionsParamBuilder(TextSearchOptionsParam param, SearchOptionsBuilder<TModel> searchOptionsBuilder, FacatedSearchMapper<TModel> queryMapper)
            : base(param, searchOptionsBuilder, queryMapper)
        {
        }

        public TextSearchOptionsParamBuilder<TModel> MapQuery<TProperty>(Expression<Func<TModel, TProperty>> property)
        {
            Enforce.Argument(() => property);

            _queryMapper.Property(property, _param.Name);
            return this;
        }

        public TextSearchOptionsParamBuilder<TModel> Disabled(bool isDisabled = true)
        {
            _param.IsDisabled = isDisabled;
            return this;
        }

        public TextSearchOptionsParamBuilder<TModel> Watermark(string watermark)
        {
            _param.Watermark = watermark;
            return this;
        }
    }
}
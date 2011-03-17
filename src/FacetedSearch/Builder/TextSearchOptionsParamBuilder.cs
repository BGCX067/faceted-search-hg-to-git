using System;
using FacetedSearch.Mapping;
using FacetedSearch.Params;
using Lokad;

namespace FacetedSearch.Builder
{
    public class TextSearchOptionsParamBuilder<TModel> : TextSearchOptionsParamBuilder where TModel : new()
    {
        private readonly FacatedSearchMapper<TModel> _queryMapper;

        public TextSearchOptionsParamBuilder(TextSearchOptionsParam param, SearchOptionsBuilder searchOptionsBuilder, FacatedSearchMapper<TModel> queryMapper)
            : base(param, searchOptionsBuilder)
        {
            _queryMapper = queryMapper;
        }

        public TextSearchOptionsParamBuilder<TModel> MapQuery(Action<FacatedSearchMapper<TModel>> action)
        {
            Enforce.Argument(() => action);

            action(_queryMapper);
            return this;
        }

    }

    public class TextSearchOptionsParamBuilder :
        BaseSearchOptionsParamBuilder<TextSearchOptionsParam, TextSearchOptionsParamBuilder>
    {
        public TextSearchOptionsParamBuilder(TextSearchOptionsParam param, SearchOptionsBuilder searchOptionsBuilder)
            : base(param, searchOptionsBuilder)
        {
            _param = param;
        }

        public TextSearchOptionsParamBuilder Disabled(bool isDisabled = true)
        {
            _param.IsDisabled = isDisabled;
            return this;
        }

        public TextSearchOptionsParamBuilder Watermark(string watermark)
        {
            _param.Watermark = watermark;
            return this;
        }
    }
}
using FacetedSearch.Params;

namespace FacetedSearch.Builder
{
    public class TextSearchOptionsParamBuilder : BaseSearchOptionsParamBuilder<TextSearchOptionsParam, TextSearchOptionsParamBuilder>
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
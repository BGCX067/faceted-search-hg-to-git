using FacetedSearch.SD;

namespace FacetedSearch.Params
{
    public class TextSearchOptionsParam : BaseSearchOptionsParam, ISD
    {
        public TextSearchOptionsParam()
        {
        }

        public TextSearchOptionsParam(string searchOptionsName) : base(searchOptionsName)
        {
            Text = string.Empty;
            Watermark = string.Empty;
        }

        public string Text { get; set; }

        public string Watermark { get; set; }

        public bool IsDisabled { get; set; }

        public override SearchOptionsParamType ParamType
        {
            get { return SearchOptionsParamType.Text; }
        }
    }
}
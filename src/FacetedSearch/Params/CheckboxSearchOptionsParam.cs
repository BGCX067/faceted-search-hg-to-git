using FacetedSearch.SD;

namespace FacetedSearch.Params
{
    public class CheckboxSearchOptionsParam : BaseSearchOptionsParam, ISD
    {
        public CheckboxSearchOptionsParam()
        {
        }

        public CheckboxSearchOptionsParam(string searchOptionsName) : base(searchOptionsName)
        {
        }

        public string Text { get; set; }

        public bool IsDisabled { get; set; }

        public bool IsChecked { get; set; }

        public override SearchOptionsParamType ParamType
        {
            get { return SearchOptionsParamType.Checkbox; }
        }
    }
}
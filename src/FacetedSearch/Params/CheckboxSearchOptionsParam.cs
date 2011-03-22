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
            Value = string.Empty;
        }

        public string Value { get; set; }

        public bool IsDisabled { get; set; }

        public bool IsChecked { get; set; }

        public override SearchOptionsParamType ParamType
        {
            get { return SearchOptionsParamType.Checkbox; }
        }
    }
}
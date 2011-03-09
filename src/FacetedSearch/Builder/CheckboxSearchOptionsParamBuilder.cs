using FacetedSearch.Params;

namespace FacetedSearch.Builder
{
    public class CheckboxSearchOptionsParamBuilder : BaseSearchOptionsParamBuilder<CheckboxSearchOptionsParam, CheckboxSearchOptionsParamBuilder>
    {
        public CheckboxSearchOptionsParamBuilder(CheckboxSearchOptionsParam param, SearchOptionsBuilder searchOptionsBuilder)
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
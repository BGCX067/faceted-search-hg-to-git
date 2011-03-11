using FacetedSearch.Params;

namespace FacetedSearch.Tests.Params
{
    public class TestSearchOptionsParam : BaseSearchOptionsParam
    {
        private SearchOptionsParamType _paramType;
        public override SearchOptionsParamType ParamType
        {
            get { return _paramType; }
            
        }

        public void SetParamType(SearchOptionsParamType value)
        {
            _paramType = value;
        }

        public bool IsDisabled { get; set; }
    }
}
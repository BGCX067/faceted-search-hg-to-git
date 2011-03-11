using FacetedSearch.Builder;
using FacetedSearch.Tests.Params;

namespace FacetedSearch.Tests.Builder
{
    public class TestSearchOptionsParamBuilder :
        BaseSearchOptionsParamBuilder<TestSearchOptionsParam, TestSearchOptionsParamBuilder>
    {
        public TestSearchOptionsParamBuilder(TestSearchOptionsParam param, SearchOptionsBuilder searchOptionsBuilder)
            : base(param, searchOptionsBuilder)
        {
            _param = param;
        }

        public TestSearchOptionsParam Param
        {
            get { return _param; }
        }

        public object SearchOptionsBuilder
        {
            get { return _searchOptionsBuilder; }
        }

        public TestSearchOptionsParamBuilder Disabled(bool isDisabled = true)
        {
            _param.IsDisabled = isDisabled;
            return this;
        }
    }
}
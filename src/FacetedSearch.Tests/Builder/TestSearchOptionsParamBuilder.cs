using FacetedSearch.Builder;
using FacetedSearch.Mapping;
using FacetedSearch.Tests.Params;

namespace FacetedSearch.Tests.Builder
{
    public class TestSearchOptionsParamBuilder :
        BaseSearchOptionsParamBuilder<TestSearchOptionsParam, TestSearchOptionsParamBuilder, object>
    {
        public TestSearchOptionsParamBuilder(TestSearchOptionsParam param, SearchOptionsBuilder<object> searchOptionsBuilder, FacatedSearchMapper<object> queryMapper)
            : base(param, searchOptionsBuilder, queryMapper)
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
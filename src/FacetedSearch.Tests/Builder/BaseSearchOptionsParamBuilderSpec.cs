using FacetedSearch.Builder;
using FacetedSearch.Mapping;
using FacetedSearch.Tests.Params;
using Machine.Specifications;

namespace FacetedSearch.Tests.Builder
{
    using Json;

    [Subject("BaseSearchOptionsParamBuilder common functionality")]
    public class base_search_options_params_builder_common_functionality : BaseSearchOptionsParamBuilderSpec
    {
        //private Behaves_like<JsonSerializerBehavior> a_json_serializer;

        private Establish context = Init;

        private Because of = () =>
                                 {
                                     
                                 };

        private It should_builder_param_equal_input_param = () => _builder.Param.ShouldEqual(_testParam);
        private It should_builder_search_options_builder_equal_input_search_options = () => _builder.SearchOptionsBuilder.ShouldEqual(_searchBiulder);
    }

    [Subject("BaseSearchOptionsParamBuilder End building param")]
    public class base_search_options_params_end : BaseSearchOptionsParamBuilderSpec
    {
        //private Behaves_like<JsonSerializerBehavior> a_json_serializer;

        private Establish context = Init;

        private Because of = () =>
                                 {
                                     _searchOptionsBuilder = _builder.End();
                                 };

        private It should_builder_param_equal_input_param = () => _searchOptionsBuilder.ShouldEqual(_searchOptionsBuilder);

        private static SearchOptionsBuilder<object> _searchOptionsBuilder;
    }
    
    [Subject("BaseSearchOptionsParamBuilder Description param")]
    public class base_search_options_params_builder_description : BaseSearchOptionsParamBuilderSpec
    {
        private Establish context = Init;

        private Because of = () => _builder.Description(_description);

        private It should_build_param_description = () => _builder.Param.Description.ShouldEqual(_description);

        private static string _description = "Test description!`'";
    }
    
    [Subject("BaseSearchOptionsParamBuilder Help param")]
    public class base_search_options_params_builder_help : BaseSearchOptionsParamBuilderSpec
    {
        private Establish context = Init;

        private Because of = () => _builder.Help(_helpText);

        private It should_build_param_help = () => _builder.Param.Help.ShouldEqual(_helpText);

        private static string _helpText = "Help text!@";
    }
    
    [Subject("BaseSearchOptionsParamBuilder Order param")]
    public class base_search_options_params_builder_order : BaseSearchOptionsParamBuilderSpec
    {
        private Establish context = Init;

        private Because of = () => _builder.Order(_orderIndex);

        private It should_build_param_order = () => _builder.Param.Order.ShouldEqual(_orderIndex);
        //private It should_search_options_builder_items_dictionary_order = () => _searchBiulder..Order.ShouldEqual(_orderIndex);

        private static int _orderIndex = 2;
    }

    public abstract class BaseSearchOptionsParamBuilderSpec
    {
        protected static IJsonSerializer _jsonSerializer;
        protected static string _jsonString;
        protected static ISearchOptions _searchOptions;
        protected static TestSearchOptionsParam _testParam;
        protected static SearchOptionsBuilder<object> _searchBiulder;
        protected static TestSearchOptionsParamBuilder _builder;
        protected static FacatedSearchMapper<object> _queryMapper;

        protected static void Init()
        {
            _testParam = new TestSearchOptionsParam();
            _jsonSerializer = new DefaultJsonSerializer();
            _searchBiulder = new SearchOptionsBuilder<object>();
            _queryMapper = new FacatedSearchMapper<object>();
            _builder = new TestSearchOptionsParamBuilder(_testParam, _searchBiulder, _queryMapper);
            _searchOptions = new SearchOptions();
        }
    }


    [Behaviors]
    public class JsonSerializerBehavior
    {
        protected static IJsonSerializer _jsonSerializer;
        protected static string _jsonString;

        private It should_be_a_json_serializer_object = () => _jsonSerializer.ShouldNotBeNull();
        private It should_be_a_json_string = () => _jsonString.Length.ShouldBeGreaterThanOrEqualTo(0);
    }
}
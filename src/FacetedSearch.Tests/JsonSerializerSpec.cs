using System.Collections.Generic;
using FacetedSearch.Params;
using Machine.Specifications;

namespace FacetedSearch.Tests
{
    [Subject("Json serializer")]
    public class json_serializer_serialize_correct_object : JsonSerializerSpecs
    {
        private Behaves_like<JsonSerializerBehavior> a_json_serializer;

        private Establish context = () =>
                                        {
                                            _jsonSerializer = new JsonSerializer();
                                            _searchParams = new List<ISearchOptionsParam>();
                                        };

        private Because of = () => { _jsonString = _jsonSerializer.Serialize(_searchParams); };
    }

    public abstract class JsonSerializerSpecs
    {
        protected static IJsonSerializer _jsonSerializer;
        protected static string _jsonString;
        protected static IList<ISearchOptionsParam> _searchParams;
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
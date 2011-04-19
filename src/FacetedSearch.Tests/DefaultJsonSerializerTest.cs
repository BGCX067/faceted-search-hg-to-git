namespace FacetedSearch.Tests
{
    using FacetedSearch.Builder;
    using NUnit.Framework;
    using SD;

    [TestFixture]
    public class DefaultJsonSerializerTest
    {
        [Test]
        public void CheckSerializationDeserialization()
        {
            IJsonSerializer serializer = new DefaultJsonSerializer();

//            var searchOptions = new SearchOptions(serializer);

            var searchOptions = FluentSearchOptions.Configure().Text("Name").End().BuildSearchOptions();

            var json = searchOptions.GetJson();

            Assert.That(searchOptions, Is.Not.Null);
            Assert.That(json, Is.Not.Null);
            Assert.That(json, Is.Not.Empty);

            var searchOptionsSD = serializer.Deserialize<SearchOptionsSD>(json);

            Assert.That(searchOptionsSD, Is.Not.Null);
        }
    }
}
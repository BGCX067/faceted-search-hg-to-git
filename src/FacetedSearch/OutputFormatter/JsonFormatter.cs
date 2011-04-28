namespace FacetedSearch.OutputFormatter
{
    using Json;

    public class JsonFormatter
    {
        private readonly IJsonSerializer _jsonSerializer;

        public JsonFormatter() : this(null)
        {
        }

        public JsonFormatter(IJsonSerializer jsonSerializer)
        {
            _jsonSerializer = jsonSerializer ?? new DefaultJsonSerializer();
        }

        public string GetJson<TModel>(SearchOptions<TModel> searchOptions, string htmlData, string htmlContainerName)
        {
            var searchOptionsSD = searchOptions.GetSDObject();
            searchOptionsSD.HtmlContainerName = htmlContainerName;
            searchOptionsSD.HtmlData = htmlData;
            return _jsonSerializer.Serialize(searchOptionsSD);
        }
    }
}
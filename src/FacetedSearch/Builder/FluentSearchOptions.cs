namespace FacetedSearch.Builder
{
    public class FluentSearchOptions : IFluentSearchOptions
    {
        public static SearchOptionsBuilder Configure()
        {
            return new SearchOptionsBuilder();
        }
    }
}
namespace FacetedSearch.Builder
{
    public class FluentSearchOptions : IFluentSearchOptions
    {
        public static SearchOptionsBuilder Configure()
        {
            return new SearchOptionsBuilder();
        }

        public static SearchOptionsBuilder<TModel> Configure<TModel>() where TModel : new()
        {
            return new SearchOptionsBuilder<TModel>();
        }
    }
}
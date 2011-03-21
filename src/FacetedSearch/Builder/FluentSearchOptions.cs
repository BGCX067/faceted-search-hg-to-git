namespace FacetedSearch.Builder
{
    public class FluentSearchOptions : IFluentSearchOptions
    {
        public static SearchOptionsBuilder<object> Configure()
        {
            return new SearchOptionsBuilder<object>();
        }

        public static SearchOptionsBuilder<TModel> Configure<TModel>() where TModel : new()
        {
            return new SearchOptionsBuilder<TModel>();
        }
    }
}
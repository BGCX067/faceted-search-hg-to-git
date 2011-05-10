namespace FacetedSearch.Factory
{
    using System;
    using Params;
    using SD;

    public class JsonSearchOptionsFactory : SearchOptionsFactory
    {
        /*public SearchOptions GetSearchOptions(SearchOptionsSD searchOptionsSD)
        {
            if (searchOptionsSD == null)
            {
                throw new ArgumentException("Invalid parameter type. Not implemented SearchOptionsSD");
            }
            var searchOptions = new SearchOptions();
            AssignSearchOptionsData(searchOptions, searchOptionsSD);
            return searchOptions;
        }

        private static SearchOptions AssignSearchOptionsData(SearchOptions searchOptions, SearchOptionsSD searchOptionsSD)
        {
            searchOptionsSD.Items.ForEach(_ => searchOptions.AddParam(_ as ISearchOptionsParam));
            return searchOptions;
        }

        public override SearchOptions GetSearchOptions<T>(T sd)
        {
            if (!typeof (T).IsAssignableFrom(typeof (SearchOptionsSD)))
            {
                throw new ArgumentException("Invalid parameter type. Not implemented SearchOptionsSD");
            }

            var searchOptionsSD = sd as SearchOptionsSD;
            return GetSearchOptions(searchOptionsSD);
        }*/
    }
}
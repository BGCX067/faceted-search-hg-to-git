using System.Web.Mvc;

namespace FacetedSearch.Web
{
    public static class FacetedSearchHtmlHelper
    {
        private const string Template = @"{0}";

        public static MvcHtmlString FacetedSearch<TModel>(this HtmlHelper<TModel> htmlHelper, SearchOptions searchOptions)
        {
            return MvcHtmlString.Create(searchOptions == null ? string.Empty : string.Format(Template, searchOptions.GetJson()));
        }
    }
}
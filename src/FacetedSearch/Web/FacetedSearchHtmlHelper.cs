namespace FacetedSearch.Web
{
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using Params;

    public static class FacetedSearchHtmlHelper
    {
        public static MvcHtmlString FacetedSearch<TModel>(this HtmlHelper<TModel> htmlHelper,
                                                          SearchOptions searchOptions)
        {
            if (searchOptions == null)
            {
                return MvcHtmlString.Empty;
            }

            return
                MvcHtmlString.Create(
                    Html.RenderSearchOptions(searchOptions)
                        .Aggregate(new StringBuilder(),
                                   (sb, block) => sb.Append(block.Render()))
                        .ToString());
        }

        public static MvcHtmlString RenderCheckbox<TModel>(this HtmlHelper<TModel> htmlHelper,
                                                           CheckboxSearchOptionsParam param)
        {
            return MvcHtmlString.Create(Html.RenderCheckbox(param).Render());
        }

        public static MvcHtmlString RenderTextbox<TModel>(this HtmlHelper<TModel> htmlHelper,
                                                          TextSearchOptionsParam param)
        {
            return MvcHtmlString.Create(Html.RenderTextbox(param).Render());
        }
    }
}
using System;
using System.Text;
using System.Web.Mvc;
using FacetedSearch.Params;

namespace FacetedSearch.Web
{
    public static class FacetedSearchHtmlHelper
    {
        private const string Template = @"{0}";

        public static MvcHtmlString FacetedSearch<TModel>(this HtmlHelper<TModel> htmlHelper, SearchOptions searchOptions)
        {
            if (searchOptions == null)
            {
                return MvcHtmlString.Create(string.Empty);
            }
            
        
            var sb = new StringBuilder();
            foreach (var item in searchOptions.GetParams())
            {
                switch (item.ParamType)
                {
                    case SearchOptionsParamType.Text:
                        sb.Append(WriteItem((TextSearchOptionsParam) item));
                        break;
                    case SearchOptionsParamType.Checkbox:
                        sb.Append(WriteItem((CheckboxSearchOptionsParam)item));
                        break;
                }
                
                
            }
            return MvcHtmlString.Create(string.Format(Template, searchOptions.GetJson()));
        }

        private static string WriteItem(CheckboxSearchOptionsParam item)
        {
            throw new NotImplementedException();
        }

        private static string WriteItem(TextSearchOptionsParam item)
        {
            throw new NotImplementedException();
        }
    }
}
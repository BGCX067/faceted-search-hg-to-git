using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.UI;
using FacetedSearch.Builder.Tag;
using FacetedSearch.Extensions;
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
                        sb.Append(RenderText(htmlHelper, (TextSearchOptionsParam)item));
                        break;
                    case SearchOptionsParamType.Checkbox:
                        sb.Append(RenderCheckbox(htmlHelper, (CheckboxSearchOptionsParam)item));
                        break;
                }
                
                
            }
            return MvcHtmlString.Create(string.Format(Template, searchOptions.GetJson()));
        }

        public static MvcHtmlString RenderCheckbox<TModel>(this HtmlHelper<TModel> htmlHelper, CheckboxSearchOptionsParam item)
        {
            IDictionary<string, object> htmlAttributes = new Dictionary<string, object>();
            htmlAttributes.Add(HtmlTextWriterAttribute.Id, item.Name);
            if (item.IsDisabled)
            {
                htmlAttributes.Add(HtmlTextWriterAttribute.Disabled, HtmlTextWriterAttribute.Disabled);
            }

            var labelBuilder = new HtmlTagBuilder(HtmlTextWriterTag.Label);

            labelBuilder.MergeAttribute(HtmlTextWriterAttribute.Value, item.Text);


            return htmlHelper.CheckBox(item.Name, item.IsChecked, htmlAttributes);
        }

        public static MvcHtmlString RenderText<TModel>(this HtmlHelper<TModel> htmlHelper, TextSearchOptionsParam item)
        {
            throw new NotImplementedException();
        }
    }
}
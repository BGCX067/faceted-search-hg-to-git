using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.UI;

namespace FacetedSearch.Extensions
{
    public static class TagBuilderExtensions
    {
        public static void MergeAttribute(this TagBuilder tagBuilder, HtmlTextWriterAttribute attribute, object value,
                                          bool replaceExisting = false)
        {
            tagBuilder.MergeAttribute(attribute.ToString(), value.ToString(), replaceExisting);
        }
        public static void MergeAttributes<TKey, TValue>(this TagBuilder tagBuilder, IDictionary<TKey, TValue> attributes,
                                          bool replaceExisting = false)
        {
            tagBuilder.MergeAttributes(attributes, replaceExisting);
        }
    }
}
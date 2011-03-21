using System.Collections.Generic;
using System.Web.UI;

namespace FacetedSearch.Extensions
{
    public static class IDictionaryExtensions
    {
        public static void Add(this IDictionary<string, object> dictionary, HtmlTextWriterAttribute attribute,
                               object value)
        {
            dictionary.Add(attribute.ToString(), value);
        }
    }
}